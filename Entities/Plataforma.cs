using System.Data;
using System.Data.OracleClient;

namespace Petrol.Entities
{
    public class Plataforma
    {
        public int Codigo { get; set; }
        public int CodigoTipo { get; set; }
        public string Nome { get; set; }

        public List<Plataforma> ListarPlataformas(int codidoPlataforma, int codigoTipoPlataforma, string nomePlataforma)
        {
            // Definindo a lista de Plataformas (com base na classe).
            List<Plataforma> listaPlataformas = new List<Plataforma>();

            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure a ser executada para obter a lista de plataformas.
            OracleCommand cmd = new OracleCommand("PKG_PLATAFORMA.SP_LISTA_PLATAFORMAS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada na stored procedure.

            OracleParameter param1_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codidoPlataforma;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = codigoTipoPlataforma;
            cmd.Parameters.Add(param2_in);

            OracleParameter param3_in = new OracleParameter("P_PLAT_TX_NOME", OracleType.VarChar);
            param3_in.Direction = ParameterDirection.Input;
            param3_in.Value = nomePlataforma;
            cmd.Parameters.Add(param3_in);

            // Definindo o parâmetro de saída (cursor) da stored procedure.
            OracleParameter output = cmd.Parameters.Add("Cursor_Plataformas", OracleType.Cursor);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();

            // Lendo os dados retornados e adicionando-os à lista de Plataformas.
            while (reader.Read())
            {
                Plataforma plataforma = new Plataforma();
                plataforma.Codigo = reader.GetInt32(reader.GetOrdinal("PLAT_CD_CODIGO"));
                plataforma.CodigoTipo = reader.GetInt32(reader.GetOrdinal("TIPL_CD_CODIGO"));
                plataforma.Nome = reader.GetString(reader.GetOrdinal("PLAT_TX_NOME"));
                
                listaPlataformas.Add(plataforma);
            }

            conn.Close();

            // Retornando a lista de Plataformas.
            return listaPlataformas;
        }

        public string Adicionar(Plataforma infoPlataforma)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para adicionar o registro.
            OracleCommand cmd = new OracleCommand("PKG_PLATAFORMA.SP_ADICIONAR_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param1_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoPlataforma.CodigoTipo;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_PLAT_TX_NOME", OracleType.VarChar);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoPlataforma.Nome;
            cmd.Parameters.Add(param2_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
 
            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public string Editar(Plataforma infoPlataforma)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para editar o registro.
            OracleCommand cmd = new OracleCommand("PKG_PLATAFORMA.SP_EDITAR_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param0_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param0_in.Direction = ParameterDirection.Input;
            param0_in.Value = infoPlataforma.Codigo;
            cmd.Parameters.Add(param0_in);

            OracleParameter param1_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoPlataforma.CodigoTipo;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_PLAT_TX_NOME", OracleType.VarChar);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoPlataforma.Nome;
            cmd.Parameters.Add(param2_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public void Excluir(int codigo)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para excluir o registro.
            OracleCommand cmd = new OracleCommand("PKG_PLATAFORMA.SP_EXCLUIR_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo o parâmetro de entrada da stored procedure.
            OracleParameter param1_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codigo;
            cmd.Parameters.Add(param1_in);

            // Executando o procedimento de exclusão do registro.
            cmd.ExecuteNonQuery();

            conn.Close();
        }

    }

}



