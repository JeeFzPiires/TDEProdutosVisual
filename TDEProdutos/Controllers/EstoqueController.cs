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

    public class EstoqueController : Controller
    {
        private IList<Produto> ListaProdutos;

        public EstoqueController()
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
                CategoriaProdutos = "Bolachas",
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
                CategoriaProdutos = "Bolachas",
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
                CategoriaProdutos = "Bolachas",
                Ativo = true

            });

        }

            [HttpPost("DebitarEstoque")]

            public ActionResult DebitarEstoque(Estoque estoque)
            {
                var resultado = ListaProdutos.Where(p => p.Codigo == estoque.Codigo).FirstOrDefault();

                if (resultado == null)
                {
                    return NotFound("O produto não existe na base de dados");
                }

                if (estoque.Qtde > resultado.EstoqueAtual)
                {
                    return BadRequest("O produto não tem estoque suficiente");
                }

                resultado.EstoqueAtual = resultado.EstoqueAtual - estoque.Qtde;

                if (resultado.EstoqueAtual < resultado.EstoqueMinimo)
                {
                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("departamentocompras@supermecados.com", "compras123");
                    }

                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new System.Net.Mail.MailAddress("departamentocompras@supermecados.com");
                        mail.To.Add(new System.Net.Mail.MailAddress("departamentocompras@supermecados.com"));


                        mail.Subject = "Estoque Minimo";
                        mail.Body = "Olá, atenção. O estoque " + resultado.EstoqueAtual + " ta baixo!";
                    }
                }

                return Ok("Produto debitado do estoque com sucesso!");

            }
    }
}
