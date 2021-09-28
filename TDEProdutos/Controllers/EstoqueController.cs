using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.models;
using TDEProdutos.Validations;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        
        [HttpPut ("Estoque/{Codigo}")]
        public ActionResult debitarEstoque(int EstoqueAtual, int EstoqueMinimo)
        {
            var resultado = ListaProdutos.Where(E => E.EstoqueAtual == EstoqueAtual).FirstOrDefault();
            if (resultado == (EstoqueAtual <= EstoqueMinimo))
            {
                return NotFound("Estoque Atual menor que o minimo, favor aumentar o estoque ");

            }

            return NoContent(resultado);
        }
    }
}