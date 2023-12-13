using AutoMapper;
using ToDoApp.Models;

namespace ToDoApp.Profiles
{
    public class TodoProfile:Profile
    {
        public TodoProfile() 
        {
            CreateMap<AddItemDTO, TodoItem>();
        }
    }
}
