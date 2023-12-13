using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using AutoMapper;
using System;

namespace ToDoApp.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public ToDoAppController(IMapper mapper)
        {
            _response = new ResponseDTO();// create the instance
            _mapper = mapper;// created instance
        }
        private static List<TodoItem> items = new List<TodoItem>()
        {
            new TodoItem()
            {
                Name = "Test",
                Created = DateTime.Now,
                Completed = true,

            }
        };

        //get all items
        [HttpGet]
        public ActionResult<ResponseDTO> getAllitems()
        {
            _response.Result = items;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        //get one item
        [HttpGet("{guid}")]//api/item/guid
        public ActionResult<ResponseDTO> getitem(Guid guid)
        {
            var item = items.Find(x=>x.Id==guid);
            if (item == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Item not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = item;
            return Ok(_response);
        }

        [HttpPost]
        public ActionResult<ResponseDTO> additem(AddItemDTO newitem)
        { 
            var newToDoItem = _mapper.Map<TodoItem>(newitem);//mapper
            _response.Result = "Item added!";
            _response.StatusCode = HttpStatusCode.Created;
            items.Add(newToDoItem);
            return Created($"api/item/{newToDoItem.Id}", _response);
        }

        [HttpPatch("{id}")]
        public ActionResult<ResponseDTO> updateitem(Guid id, AddItemDTO Updateitem)
        {
            var item = items.Find(x => x.Id == id);
            if (item == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Item not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            //update
            _mapper.Map(Updateitem, item);

            _response.Result = item;
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseDTO> deleteitem(Guid id)
        {
            var item = items.Find(x => x.Id == id);
            if (item == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Item not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            //delete
            items.Remove(item);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = "Item deleted";
            return NoContent();
        }
    }
}
