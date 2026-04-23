using Core.DTO;
using Core.IReprosatory;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reprosatory
{
    public class CategoryRepo : ICatrgoryRepo
    {
        private readonly DataContext dataContext;

        public CategoryRepo(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        void ICatrgoryRepo.AddCategory(CategoryD caegory)
        {
            Category category = new Category
            {
                Name = caegory.Name
            };
            dataContext.Categories.Add(category);
            dataContext.SaveChanges();
        }

        List<CategoryD> ICatrgoryRepo.GetAllCategories()
        {
           var categories = dataContext.Categories;
            List<CategoryD> categoryDTOs = new List<CategoryD>();
            foreach (var category in categories)
            {
                CategoryD categoryDTO = new CategoryD
                {
                    Name = category.Name
                };
                categoryDTOs.Add(categoryDTO);
            }
            return categoryDTOs;
        }

        CategoryD ICatrgoryRepo.GetCategory(int id)
        {
            var category = dataContext.Categories.Find(id);
            if (category == null)
            {
                return null;
            }
            var categoryDTO = new CategoryD
            {
                Name = category.Name
            };
            return categoryDTO;
        }

        void ICatrgoryRepo.RemoveCategory(int id)
        {
            var category = dataContext.Categories.Find(id);
            if (category != null)
            {
                dataContext.Categories.Remove(category);
                dataContext.SaveChanges();
            }

        }

        void ICatrgoryRepo.UpdateCategory(CategoryD caegory, int id)
        {
            var category = dataContext.Categories.Find(id);
            if (category != null)
            {
                category.Name = caegory.Name;
                dataContext.SaveChanges();
            }
        }
    }
}
