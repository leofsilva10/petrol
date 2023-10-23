using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.Producao
{
    public class EditModel : PageModel
    {
        public Entities.Producao infoProducao = new Entities.Producao();
        public List<Entities.Producao> listaProducao = new List<Entities.Producao>();
        public List<Entities.Plataforma> listaPlataformas = new List<Entities.Plataforma>();
        public string msgErro = "";
        public string msgSucesso = "";
        public int _codigo;
        public decimal _valor;
        public int _codigoPlataforma;
        public string _data_registro;

        public void OnGet()
        {
            // Obter código da plataforma selecionada.
            string codigo = Request.Query["codigo"];

            var clsProducao = new Entities.Producao();

            // Listar plataforma a ter seus dados editados.
            listaProducao = clsProducao.ListarProducao(int.Parse(codigo), 0);

            foreach (var _producao in listaProducao)
            {
                _codigo = _producao.Codigo;
                _valor = _producao.Valor;
                _codigoPlataforma = _producao.CodigoPlataforma;
                _data_registro = _producao.DataRegistro;
                break;
            }

            var clsPlataforma = new Entities.Plataforma();

            // Listar os tipos de plataformas disponíveis.
            listaPlataformas = clsPlataforma.ListarPlataformas(0, 0, "");
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoProducao.Codigo = int.Parse(Request.Form["codigo"]);
            infoProducao.Valor = int.Parse(Request.Form["valor"]);
            infoProducao.CodigoPlataforma = int.Parse(Request.Form["codigoPlataforma"]);
            infoProducao.DataRegistro = Request.Form["data_registro"];

            // Verificar se os dados foram cadastrados corretamente.

            if (infoProducao.Valor == 0 || infoProducao.CodigoPlataforma == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Editar a plataforma.

            try
            {
                var clsProducao = new Entities.Producao();

                if (clsProducao.Editar(infoProducao) == "0")
                {
                    msgErro = "Erro ao atualizar o registro.";
                    return;
                }
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
                return;
            }

            msgSucesso = "Registro atualizado com sucesso!";

            Response.Redirect("/Producao");
        }

    }
}
