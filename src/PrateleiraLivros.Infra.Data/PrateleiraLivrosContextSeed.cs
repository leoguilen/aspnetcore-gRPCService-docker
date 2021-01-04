using Microsoft.EntityFrameworkCore;
using PrateleiraLivros.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrateleiraLivros.Data
{
    public static class PrateleiraLivrosContextSeed
    {
        public static async Task SeedAsync(PrateleiraLivrosContext dbContext)
        {
            var autores = GetAutoresPreConfigurados();
            var editoras = GetEditorasPreConfiguradas();

            if (!await dbContext.Autores.AnyAsync())
            {
                await dbContext.Autores.AddRangeAsync(autores);
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Editoras.AnyAsync())
            {
                await dbContext.Editoras.AddRangeAsync(editoras);
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Livros.AnyAsync())
            {
                await dbContext.Livros.AddRangeAsync(
                    GetLivrosPreConfigurados(autores, editoras));

                await dbContext.SaveChangesAsync();
            }
        }

        private static List<Autor> GetAutoresPreConfigurados()
        {
            var autores = new List<Autor>
            {
                new Autor
                {
                    Nome = "Machado",
                    Sobrenome = "de Assis",
                    Email = "machado.assis@gmail.com",
                    Bio = @"Nascido no Rio de Janeiro, em 21 de junho de 1839, 
                            Machado de Assis foi escritor, jornalista, contista, 
                            dramaturgo e poeta. Considerado por muitos críticos o 
                            maior nome da literatura brasileira de todos os tempos. 
                            Seu legado foi muito importante para as escolas literárias 
                            brasileiras dos séculos XIX e XX. Entre suas obras mais famosas 
                            estão Memórias póstumas de Brás Cubas, Quincas Borba e Esaú e Jacó.",
                    Avatar = "machado_assis.jpg",
                },
                new Autor
                {
                    Nome = "Aluísio",
                    Sobrenome = "Azevedo",
                    Email = "aluisio.azevedo@gmail.com",
                    Bio = @"Membro fundador da Academia Brasileira de Letras, 
                            Aluísio Azevedo nasceu em 14 de abril de 1857 em São Luís do Maranhão
                            e morreu em 21 de janeiro de 1913. Romancista, caricaturista e diplomata, 
                            mudou-se aos 19 anos para o Rio de Janeiro, onde passou a colaborar com poesias 
                            e caricaturas para jornais e revistas da cidade. De estilo romântico, seu primeiro 
                            livro Uma Lágrima de Mulher, publicado em 1880, era bem diferente do que viria a seguir. 
                            Um ano depois, lançou O Mulato, dando início ao movimento naturalista no Brasil, 
                            com influências de Émile Zola e Eça de Queiroz. Seus títulos retratavam a vida cotidiana das pessoas, 
                            incluindo temas como preconceito, adultério, vícios e pobreza. A grande obra de sua carreira, 
                            O Cortiço, foi publicada em 1890 e descreve essa habitação e os acontecimentos da vida de seus moradores.",
                    Avatar = "aluisio_azevedo.jpg",
                },
                new Autor
                {
                    Nome = "João",
                    Sobrenome = "Guimarães Rosa",
                    Email = "joao.guimaraes@gmail.com",
                    Bio = @"Nascido em 27 de junho de 1908, foi um dos mais importantes 
                            escritores brasileiros de todos os tempos. Além de escritor, 
                            atuou como médico e diplomata. Os contos e romances escritos 
                            por ele ambientam-se quase todos no sertão brasileiro, 
                            influenciado ditos populares e regionais que, somados à erudição do autor, 
                            permitiu a criação de inúmeros vocábulos a partir de arcaísmos e palavras populares. 
                            Realismo mágico, regionalismo, liberdade de invenções linguísticas 
                            e neologismos são algumas das características fundamentais da literatura de Guimarães Rosa. 
                            Alguns destaques de sua bibliografia: Sagarana, Grande sertão: Veredas, Primeiras estórias e Noites do sertão.",
                    Avatar = "joao_guimaraes.jpg",
                }
            };

            return autores;
        }

        private static List<Editora> GetEditorasPreConfiguradas()
        {
            var editoras = new List<Editora>
            {
                new Editora
                {
                    Nome = "Companhia das Letras",
                    SiteURL = "https://www.companhiadasletras.com.br/",
                    Endereco = "Rua Bandeira Paulista, 702, cj. 3204532-002 - São Paulo - SP"
                },
                new Editora
                {
                    Nome = "Alta Books",
                    SiteURL = "https://www.altabooks.com.br/",
                    Endereco = "Rua Viúva Claudio, 291 – Bairro Industrial do Jacaré – Rio de Janeiro – RJ – CEP 20970-031"
                },
                new Editora
                {
                    Nome = "Escala",
                    SiteURL = "https://www.escala.com.br/",
                    Endereco = "Av. Profª. Ida Kolb, 551 - Casa Verde CEP 02518-000 - São Paulo - SP"
                }
            };

            return editoras;
        }

        private static List<Livro> GetLivrosPreConfigurados(List<Autor> autores, List<Editora> editoras)
        {
            var livros = new List<Livro>
            {
                new Livro
                {
                    Titulo = "Dom Casmurro",
                    Descricao = "Poucos romances examinam com tanta sutileza as artimanhas do ciúme como Dom Casmurro." +
                                " Publicado em 1899, o livro permanece ainda hoje como um dos mais fascinantes estudos da traição." + 
                                " Aliás, como o leitor mais atento perceberá, são supostamente duas: a de Capitu," +
                                " exposta pelo marido Bentinho, e a própria narrativa sobre como Bentinho modifica" +
                                " os fatos para corroborar suas suspeitas matrimoniais. Tudo isso é narrado com graça" + 
                                " e inteligência num romance que jamais parece esgotar suas possibilidades de leitura." +
                                " Críticos como Roberto Schwarz e Susan Sontag consideram a obra de Machado como um dos" +
                                " momentos mais altos da prosa ocidental do final do século XIX.",
                    Valor = 37.9M,
                    ISBN_10 = 8582850352,
                    Edicao = 1,
                    DataPublicacao = DateTime.Parse("26/07/2016"),
                    Idioma = "Português",
                    NumeroPaginas = 400,
                    EditoraId = editoras[0].Id,
                    AutorId = autores[0].Id
                },
                new Livro
                {
                    Titulo = "Medo Imortal",
                    Descricao = "A antologia Medo Imortal, mais nova integrante da coleção Medo Clássico da Darkside® Books," + 
                                " vem a público para mostrar que existe mais em comum entre os fatos dos dois parágrafos" +
                                " anteriores do que pode aparentar à primeira vista. Liderados por nosso maior escritor," + 
                                " Machado de Assis, aqueles intelectuais brasileiros são pessoas de seu tempo," + 
                                " conectados com o que estava sendo produzido nos grandes centros culturais do mundo em sua época." +
                                " Nas páginas de Medo Imortal estão reunidos, além de poesias, 32 exemplares da prosa de escritores" + 
                                " diretamente ligados à nossa principal instituição dedicada à literatura. São contos que evocam o sobrenatural," +
                                " apresentam monstros, descrevem atos de psicopatas, dão o testemunho de todo tipo imaginável de atrocidades" +
                                " concebidas pela mente humana. Produzidos entre a segunda metade do século xix e" +
                                " a primeira metade do século xx, tais textos representam o que de melhor se escreveu nos" + 
                                " primeiros cem anos de produção do terror em nosso país. Organizado pelo jornalista Romeu Martins," +
                                " com ilustrações de Lula Palomanes, a lista de autores para o livro contou com a colaboração de" +
                                " estudos realizados pelos maiores pesquisadores do terror e do insólito das principais universidades" + 
                                " brasileiras. São ao todo treze autores, escolhidos entre os patronos, os fundadores e os primeiros" +
                                " eleitos para ocupar os salões da Academia Brasileira de Letras. Entre eles, a Darkside® Books aproveitou" + 
                                " a oportunidade de reparar uma injustiça histórica cometida naquele ano de 1897 e traz também contos da" +
                                " escritora Júlia Lopes de Almeida, importante nome de nossa literatura que participou das reuniões para a" +
                                " fundação da Academia mas que na última hora acabou sendo barrada por ser mulher em uma instituição que" +
                                " em seus primeiros oitenta anos só aceitou a presença de homens.",
                    Valor = 58.49M,
                    ISBN_10 = 8594541694,
                    Edicao = 1,
                    DataPublicacao = DateTime.Parse("18/06/2019"),
                    Idioma = "Português",
                    NumeroPaginas = 464,
                    EditoraId = editoras[1].Id,
                    AutorId = autores[1].Id
                },
                new Livro
                {
                    Titulo = "Manuelzão e Miguilim",
                    Descricao = "Este livro é composto por duas novelas, “Campo Geral” e “Uma estória de amor (festa de Manuelzão)”," +
                                " que de certa forma se complementam apresentando as duas pontas da existência humana:" + 
                                " a infância de Miguilim, marcada pela descoberta constante e por vezes dolorosa do mundo;" + 
                                " e a velhice do vaqueiro Manuelzão, um relembrar também por vezes doloroso do que é a vida," + 
                                " como se, de tão acostumado a ela, houvesse esquecido sua dinâmica e voltasse a se deparar com" + 
                                " a sua “espantante” novidade.",
                    Valor = 36.37M,
                    ISBN_10 = 8520925006,
                    Edicao = 1,
                    DataPublicacao = DateTime.Parse("16/05/2016"),
                    Idioma = "Português",
                    NumeroPaginas = 216,
                    EditoraId = editoras[2].Id,
                    AutorId = autores[2].Id
                }
            };

            return livros;
        }
    }
}
