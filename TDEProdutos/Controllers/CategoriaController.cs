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
    public class CategoriaController : Controller
    {
        private IList<Categoria> ListaCategoria;

        public CategoriaController()
        {
            ListaCategoria = new List<Categoria>();

            ListaCategoria.Add(new Categoria()
            {
                IDCategoria = "1",
                Descricao = "Biscoitos"
            });

            ListaCategoria.Add(new Categoria()
            {
                IDCategoria = "2",
                Descricao = "Salgadinhos"
            });

            ListaCategoria.Add(new Categoria()
            {
                IDCategoria = "3",
                Descricao = "Cerveja"
            });
        }

        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("Ola");
        }

        [HttpGet("BuscarCategoria/{IDCategoria}")]

        public ActionResult BuscarCategoria(string IDcategoria)
        {
            var resultado = ListaCategoria.Where(ID => ID.IDCategoria == IDcategoria).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound("Não existe esse codigo de categoria na base de dados");
            }

            return Ok(resultado);


        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Adicionar(Categoria categoria)
        {
            CategoriaValidation categoriaValidation = new CategoriaValidation();

            var validacao = categoriaValidation.Validate(categoria);

            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach (var failure in validacao.Errors)
                {
                    erros.Add("Property " + failure.PropertyName +
                        " failed validation. Error Was: "
                        + failure.ErrorMessage);
                }

                return BadRequest(erros);
            }

            var result = ListaCategoria.Where(C => C.IDCategoria == categoria.IDCategoria).FirstOrDefault();
            if (result != null)
                return BadRequest("Produto nao foi cadastrado, pois codigo ja existe");
            result = ListaCategoria.Where(c => c.Descricao == categoria.Descricao).FirstOrDefault();
            if (result != null)
                return BadRequest("Produto nao foi cadastrado, pois nome ja existe");


            ListaCategoria.Add(categoria);

            return CreatedAtAction(nameof(Adicionar), categoria);
        }

        [HttpPut("Atualizar/{IDCategoria}")]
        public ActionResult Atualizar(string IDCategoria, [FromBody] Categoria categoria)
        {
            var resultado = ListaCategoria.Where(c => c.IDCategoria == IDCategoria).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound();
            }

            categoria.IDCategoria = IDCategoria;
            ListaCategoria.Remove(resultado);
            ListaCategoria.Add(categoria);

            return NoContent();

        }

        [HttpDelete("Remover/{Codigo}")]
        public ActionResult Remover(string IDCategoria)
        {
            var resultado = ListaCategoria.Where(c => c.IDCategoria == IDCategoria).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound();
            }
            ListaCategoria.Remove(resultado);
            return NoContent();
        }

    }
}
