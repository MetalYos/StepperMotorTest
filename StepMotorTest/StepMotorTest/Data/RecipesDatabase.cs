using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

using StepMotorTest.Models;

namespace StepMotorTest.Data
{
    public class RecipesDatabase
    {
        string _filename;
        List<Recipe> recipes;
        int changesCounts = 0;
        const int changesLimit = 1;

        public RecipesDatabase(string dbPath)
        {
            _filename = dbPath;
            if (!File.Exists(_filename))
            {
                File.Create(_filename);
                recipes = new List<Recipe>();
            }
            else
            {
                LoadRecipes();
            }
        }

        public List<Recipe> GetRecipes()
        {
            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                    return recipe;
            }

            return null;
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            changesCounts++;
            SaveRecipes();
        }

        public void AddMoveToRecipe(Move move)
        {
            if (ContainsMove(move))
                return;

            Recipe recipe = GetRecipe(move.RecipeID);
            recipe.Moves.Add(move);
            SaveRecipes();
        }

        public bool UpdateRecipe(Recipe updatedRecipe)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == updatedRecipe.ID)
                {
                    int index = recipes.IndexOf(recipe);
                    recipes.Insert(index, updatedRecipe);
                    recipes.Remove(recipe);
                    SaveRecipes();
                    return true;
                }
            }
            
            return false;
        }

        public bool UpdateMove(Move updatedMove)
        {
            if (!ContainsMove(updatedMove))
                return false;

            Recipe recipe = GetRecipe(updatedMove.RecipeID);
            foreach (var item in recipe.Moves)
            {
                if (item.ID == updatedMove.ID)
                {
                    int index = recipe.Moves.IndexOf(item);
                    recipe.Moves.Insert(index, updatedMove);
                    recipe.Moves.Remove(item);
                    SaveRecipes();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteRecipe(int id)
        {
            Recipe recipe = GetRecipe(id);
            if (recipe == null)
                return false;

            recipes.Remove(recipe);
            SaveRecipes();
            return true;
        }

        public bool DeleteMove(Move move)
        {
            if (!ContainsMove(move))
                return false;

            Recipe recipe = GetRecipe(move.RecipeID);
            recipe.Moves.Remove(move);
            SaveRecipes();
            return false;
        }

        public bool ContainsRecipe(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                    return true;
            }

            return false;
        }

        public bool ContainsMove(Move move)
        {
            if (!ContainsRecipe(move.RecipeID))
                return false;

            Recipe recipe = GetRecipe(move.RecipeID);
            foreach (var item in recipe.Moves)
            {
                if (item.ID == move.ID)
                    return true;
            }
            return false;
        }

        public void LoadRecipes()
        {
            try
            {
                using (FileStream fs = new FileStream(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
                    recipes = (List<Recipe>)serializer.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Source + "\n" + e.Message);
                recipes = new List<Recipe>();
            }
        }

        public void SaveRecipes()
        {
            changesCounts++;
            if (changesCounts == changesLimit)
            {
                try
                {
                    using (TextWriter writer = new StreamWriter(_filename))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
                        serializer.Serialize(writer, recipes);
                        changesCounts = 0;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Source + "\n" + e.Message);
                }
            }
        }
    }
}
