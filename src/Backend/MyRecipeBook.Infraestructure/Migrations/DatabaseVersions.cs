using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infraestructure.Migrations
{
    public abstract class DatabaseVersions
    {
        // Classe para manter o controle das versões do banco de dados.
        public const int TABLE_USER = 1;
    }
}
