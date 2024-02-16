namespace Coffee;

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.IO; 
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using System.Collections.Generic;


[ApiController]
public class StoreController : ControllerBase
{
    
    private readonly IPurchaseRepository _purchaseRepository;

    public StoreController(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

        [HttpPost]
        [Route("/store/add")]
        public IActionResult Add([FromBody] Purchase newPurchase)
        { 
            _purchaseRepository.AddPurchase(newPurchase);
            return Ok(_purchaseRepository.GetAllPurchases());
        }

        [HttpPost]
        [Route("/store/delete")]
        public IActionResult Delete(int id)
        {
            var purchase = _purchaseRepository.GetPurchaseById(id);
            if (purchase != null)
            {
                _purchaseRepository.DeletePurchase(id);
                return Ok($"{id} удален");
            }
            else
            {
                return NotFound($"{id} не найден");
            }
        }

        [HttpGet]
        [Route("/store/show")]
        public IActionResult Show()
        {
            return Ok(_purchaseRepository.GetAllPurchases());
        }
      

}