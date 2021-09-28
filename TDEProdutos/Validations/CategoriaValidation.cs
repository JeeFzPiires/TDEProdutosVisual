using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDEProdutos.models;


namespace TDEProdutos.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(Categoria => Categoria.IDCategoria)
                .NotEmpty().WithMessage("campo categoria vazio")
                .NotNull().WithMessage("campo categoria não informado!")
                .Must(SomenteNumero).WithMessage("Codigo Inválido");
        }
        public static bool SomenteNumero(string Numeros)
        {
            //logica da receita
            return Regex.IsMatch(Numeros, @"^[0-9]+$");

        }
    }
}
