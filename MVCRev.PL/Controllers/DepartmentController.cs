using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRev.BLL.Interfaces;
using MVCRev.DAL.Models;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private IDepartmentRepository _departmentRepository;


        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }




        public async Task< IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(Department model) {
        
        if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(model);
                var count = await _unitOfWork.Copelete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

        return View(model);
        
        
        }



        public async Task< IActionResult>  Details(int? id , string ViewName = "Details")
        {
            if(id is null)
            {
                return BadRequest();
            }

            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);


            if(department is null)
            {
                return NotFound();
            }


            return View(ViewName, department);
        }

        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {

            return await Details(id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int? id , Department model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                
                _unitOfWork.DepartmentRepository.Update(model);
                var count = await _unitOfWork.Copelete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);


        }


        [HttpGet]
        public async Task< IActionResult> Delete(int? id) {
            return await  Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Delete([FromRoute] int? id , Department model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if(ModelState.IsValid)
            {
                 _unitOfWork.DepartmentRepository.Delete(model);
                var count = await _unitOfWork.Copelete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    }


}
