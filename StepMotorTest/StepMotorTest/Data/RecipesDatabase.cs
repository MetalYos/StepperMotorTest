using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;

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
                // Desrialize
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

        public void SaveRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            changesCounts++;
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
                    changesCounts++;
                    SaveRecipes();
                    return true;
                }
            }
            
            return false;
        }

        public bool DeleteRecipe(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                {
                    recipes.Remove(recipe);
                    changesCounts++;
                    SaveRecipes();
                    return true;
                }
            }

            return false;
        }

        public bool Contains(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                    return true;
            }

            return false;
        }

        private void LoadRecipes()
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
                recipes = new List<Recipe>();
            }
        }

        private void SaveRecipes()
        {
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
                    int t = 7;
                }
            }
        }
    }
}
