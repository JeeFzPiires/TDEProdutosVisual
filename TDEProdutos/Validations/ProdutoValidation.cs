using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDEProdutos.models;

namespace TDEProdutos.Validations
{
    public class ProdutoValidation: AbstractValidator<Produto>

    {

        public ProdutoValidation()
        {
            RuleFor(Produto => Produto.Codigo)
               .NotEmpty().WithMessage("Campo codigo vazio,tente novamente ")
               .NotNull().WithMessage("Campo codigo não informado, tente novamente !")
               .Must(SomenteNumero).WithMessage("codigo invalido");


            RuleFor(Produto => Produto.Nome)
                .NotEmpty().WithMessage("campo nome vazio")
                .NotNull().WithMessage("Campo nome não informado ")
                .MaximumLength(500).WithMessage("tamanho maximo excedido!")
                .Must(SomenteLetras).WithMessage("Somente Letras");

            RuleFor(Produto => Produto.PrecoVendas)
                .NotEmpty().WithMessage("campo preço vazio")
                .NotNull().WithMessage("campo preço não informado,Favor inserir !")
                .GreaterThan(0).WithMessage("preço deve ser maior que zero");
           

            RuleFor(Produto => Produto.Ativo)
                .NotEmpty().WithMessage("campo produto ativo ou inativo vazio!")
                .NotNull().WithMessage("campo produto ativo ou inativo não informado,Favor inserir");

            RuleFor(Produto => Produto.DataCadastro)
                .NotEmpty().WithMessage("campo data vazio")
                .NotNull().WithMessage("campo data não informado");

            RuleFor(Produto => Produto.Descricao)
                .NotEmpty().WithMessage("campo descrição vazio")
                .NotNull().WithMessage("campo descrição não informado");

               

        }

        public static bool SomenteNumero(string Numeros)
        {
            //logica da receita
            return  Regex.IsMatch(Numeros, @"^[0-9]+$");

        }

        public static bool SomenteLetras(string palavra)
        {
            return Regex.IsMatch(palavra, @"^[a-z A-Z]+$");
        }




    }
}
