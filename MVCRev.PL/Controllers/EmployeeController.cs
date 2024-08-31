using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRev.BLL.Interfaces;
using MVCRev.BLL.Repositories;
using MVCRev.DAL.Models;
using MVCRev.PL.Helper;
using MVCRev.PL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{

	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;
        //private IEmployeeRepository _employeeRepository { get; set; }




        public EmployeeController( IUnitOfWork unitOfWork ,  IMapper mapper  /*IEmployeeRepository employeeRepository*/   /* IDepartmentRepository departmentRepository*/  )
        {
             _unitOfWork = unitOfWork;
             _mapper = mapper;


            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
        }


        public async Task< IActionResult> Index(string searchInput)
        {
            var Employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInput))
            {

             Employee = await _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                 Employee =   _unitOfWork.EmployeeRepository.GetByName(searchInput);
            }

         var Result =    _mapper.Map<IEnumerable< EmployeeViewModel>>(Employee);
            

            return View(Result);
        }




        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _unitOfWork.DepartmentRepository.GetAll();
            return View();

        }



        [HttpPost]
        public async Task< IActionResult > Create(EmployeeViewModel model )
        {
            if(ModelState.IsValid)
            {
                #region Old Mapper
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    HiringDate = model.HiringDate,
                //    Salary = model.Salary,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Email = model.Email,
                //    Phone = model.Phone,
                //    DateOfCreation = model.DateOfCreation,
                //    DepartmentId = model.DepartmentId,
                //    Department = model.Department,
                //    IsDeleted = model.IsDeleted,


                //};

                #endregion
                model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                var employee = _mapper.Map<Employee>(model);
                 _unitOfWork.EmployeeRepository.Add(employee);
                var count = await _unitOfWork.Copelete();
                if (count > 0)

                {
                    TempData["Message"] = "Employee Added "; 
                }
                else
                {
                    TempData["Message"] = "Employee NOT Added ";

                }
                    return RedirectToAction(nameof(Index));
            }
            return View(model);

        }



        public async Task< IActionResult > Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = await _unitOfWork.EmployeeRepository.GetById(id.Value);


            if (employee is null)
            {
                return NotFound();
            }


            var Result = _mapper.Map<EmployeeViewModel>(employee) ; 


            return View(ViewName, Result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Departments"] = await _unitOfWork.DepartmentRepository.GetAll();

            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult > Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (model.ImageName is not null)
            {
                DocumentSetting.DeleteFiel(model.ImageName, "images");

            }


            model.ImageName = DocumentSetting.UploadFile(model.Image, "images");

            var employee = _mapper.Map<Employee>(model) ;


            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.Copelete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);


        }


        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var employee = _mapper.Map<Employee>(model);

            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.Copelete();

                if (count > 0)
                {

                    DocumentSetting.DeleteFiel(model.ImageName, "images");

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    
    
    
    
    }
}
