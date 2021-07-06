using FastMember;
using GiphyAPIContract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiphyAPITest
{
    public partial class TestGiphyAPI : Form
    {
        Dictionary<string, List<Gif>> SearchesGipfy = new Dictionary<string, List<Gif>>();
        public TestGiphyAPI()
        {
            InitializeComponent();
        }

        private void btnTrending_Click(object sender, EventArgs e)
        {
            ChannelFactory<IGiphyService> channel = new ChannelFactory<IGiphyService>("giphyEndpoint");
            IGiphyService proxy = channel.CreateChannel();

            List<Gif> gifs= proxy.GetAllGiphy();


            channel.Close();
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(gifs))
            {
                table.Load(reader);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchesGipfy.ContainsKey(txtSearch.Text))
            {
                
                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(SearchesGipfy[txtSearch.Text]))
                {
                    table.Load(reader);
                }
            }
            else
            {
                ChannelFactory<IGiphyService> channel = new ChannelFactory<IGiphyService>("giphyEndpoint");
                IGiphyService proxy = channel.CreateChannel();

                List<Gif> gifs = proxy.SearchGigphy(txtSearch.Text);

                channel.Close();
                SearchesGipfy.Add(txtSearch.Text, gifs);
                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(gifs))
                {
                    table.Load(reader);
                }
            }
        }
    }
}
