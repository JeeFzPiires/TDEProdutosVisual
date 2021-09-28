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

    public class ProdutosController : Controller
    {
        private IList<Produto> ListaProdutos;

        public ProdutosController()
        {
            ListaProdutos = new List<Produto>();

            ListaProdutos.Add(new Produto()
            {
                Nome = "Biscoito Recheado de Morango",
                Codigo = "1234",
                Descricao = "Bolacha de Morango",
                PrecoVendas = 2.00F,
                PrecoCusto = 0.99F,
                DataCadastro = new DateTime(2021, 08, 25),
                Estoque = 100,
                Imagem = "imagemBolacha.png",
                AlturaCentimetros = 19,
                LarguraCentimetros = 4,
                ProfundidadeCentimetros = 4,
                //Categoria =
                Ativo = true

            });
            ListaProdutos.Add(new Produto()
            {
                Nome = "Salgadinho De Milho Nacho DORITOS Queijo Pacote 55g",
                Codigo = "1235",
                Descricao = "Doritos",
                PrecoVendas = 2.00F,
                PrecoCusto = 0.99F,
                DataCadastro = new DateTime(2021, 08, 25),
                Estoque = 100,
                Imagem = "imagemDoritos.png",
                AlturaCentimetros = 19,
                LarguraCentimetros = 4,
                ProfundidadeCentimetros = 4,
                //Categoria = "Bolachas",
                Ativo = true

            });
            ListaProdutos.Add(new Produto()
            {
                Nome = "Biscoito Recheado de Morango com Chocolate",
                Codigo = "1236",
                Descricao = "Bolacha de Morango com Chocolate",
                PrecoVendas = 2.00F,
                PrecoCusto = 0.99F,
                DataCadastro = new DateTime(2021, 08, 25),
                Estoque = 100,
                Imagem = "imagemCerveja.png",
                AlturaCentimetros = 19,
                LarguraCentimetros = 4,
                ProfundidadeCentimetros = 4,
                //Categoria = "Bolachas",
                Ativo = true,
                EstoqueAtual = 100,
                EstoqueMinimo = 20

            });


        }

        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("Ola");
        }

        [HttpGet("BuscarPorCodigo/{Codigo}")]

        public ActionResult BuscarPorCodigo(string codigo)
        {
            var resultado = ListaProdutos.Where(P => P.Codigo == codigo).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound("O produto não existe na base de dados");
            }
           
            return Ok(resultado);
        }


        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Adicionar(Produto Produto)
        {
            ProdutoValidation produtoValidation = new ProdutoValidation();

            var validacao = produtoValidation.Validate(Produto);

            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach(var failure in validacao.Errors)
                {
                    erros.Add("Property " + failure.PropertyName +
                        " failed validation. Error Was: "
                        + failure.ErrorMessage);
                }

                return BadRequest(erros);
            }

            var r = ListaProdutos.Where(p => p.Codigo == Produto.Codigo).FirstOrDefault();
            if (r != null)
                return BadRequest("Produto nao foi cadastrado, pois codigo ja existe");

            r = ListaProdutos.Where(p => p.Nome == Produto.Nome).FirstOrDefault();
            if (r != null)
                return BadRequest("Produto nao foi cadastrado, pois nome ja existe");


            ListaProdutos.Add(Produto);
            
            return CreatedAtAction(nameof(Adicionar), Produto);
        }





        [HttpPut("Atualizar/{Codigo}")]
        public ActionResult Atualizar (string Codigo, [FromBody] Produto Produto)
        {
            var resultado = ListaProdutos.Where(P => P.Codigo == Codigo).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound();
            }
            Produto.Codigo = Codigo;
            ListaProdutos.Remove(resultado);
            ListaProdutos.Add(Produto);

            var r = ListaProdutos.Where(p => p.Codigo == Produto.Codigo).FirstOrDefault();
            if (r != null)
                return BadRequest("Produto nao foi cadastrado, pois codigo ja existe");

            r = ListaProdutos.Where(p => p.Nome == Produto.Nome).FirstOrDefault();
            if (r != null)
                return BadRequest("Produto nao foi cadastrado, pois nome ja existe");

            return NoContent();

        }

        [HttpDelete("Remover/{Codigo}")]
        public ActionResult Remover(string Codigo)
        {
            var resultado = ListaProdutos.Where(P => P.Codigo ==Codigo).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound();
            }
            ListaProdutos.Remove(resultado);
            return NoContent();
        }


        [HttpPut("Desativar/{Codigo}")] //Enviar e-mail
        public ActionResult Desativar(bool Ativo, [FromBody] Produto Produto)
        {
            var resultado = ListaProdutos.Where(P => P.Ativo == Ativo).FirstOrDefault();
            if (resultado == null)
            {
                return Ok();
            }
            Produto.Ativo = Ativo;

            
           

            return NoContent();

        }


    }
}
