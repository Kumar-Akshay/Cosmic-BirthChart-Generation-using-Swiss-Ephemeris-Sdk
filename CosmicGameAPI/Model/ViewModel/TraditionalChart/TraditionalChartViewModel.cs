using System.Collections.Generic;

namespace CosmicGameAPI.Model.ViewModel.TraditionalChart
{
    public class TraditionalChartViewModel
    {
        private static string[] ZodiacSigns = new string[12]{
            "Aries-மேஷம்",
            "Tauras / இடபம்",
            "Gemini/மிதுனம்",
            "Cancer கடகம்",
            "Leo / சிங்கம்",
            "Virgo / கன்னி",
            "Libra / துலாம்",
            "Scorpi/விருச்சி",
            "Sagit. / தனுசு",
            "Capric/மகரmmmம்",
            "Aquar./ கும்பம்",
            "Pisces / மீனம்"
        };
        public List<TraditionalChartCell> Cells { get; set; }
        public TraditionalChartViewModel()
        {
            Cells = new List<TraditionalChartCell>();
            for(int i = 1; i <= 12; i++)
            {
                Cells.Add(new TraditionalChartCell() {
                    Code = GenerateHintForCell(i)
                });
            }
        }

        private string GenerateHintForCell(int kid) {
            var minDegree = (kid - 1) * 30;
            var maxDegree = kid * 30;
            var zodiacSign = ZodiacSigns[kid - 1];
            return string.Format("<span class=\"fs8\" data-toggle=\"tooltip\" title=\"[{0}] {1}-{2} {3}\">", kid, minDegree, maxDegree, zodiacSign);
        }

        public string CreateTableBody()
        {
            var table = "<table border = 2 class='table table-bordered' style='border: 2px solid #000; width: unset; margin: 0 auto;'><tbody>";
            //First Row
            table = GenerateCell(table, Cells[11].Code, "border-color:#000!important;");
            table = GenerateCell(table, Cells[0].Code, "border-color:#000;");
            table = GenerateCell(table, Cells[1].Code, "border-color:#000;");
            table = GenerateCell(table, Cells[2].Code, "border-color:#000!important;");
            table += "</tr><tr>";
            //Second Row
            table = GenerateCell(table, Cells[10].Code, "border-color:#000!important;");
            table = GenerateCell(table, "", "border-bottom:none;border-right:none;border-color:#000;");
            table = GenerateCell(table, "", "border-bottom:none;border-left:none;border-color:#000;");
            table = GenerateCell(table, Cells[3].Code, "border-color:#000!important;");
            table += "</tr><tr>";
            //Third Row
            table = GenerateCell(table, Cells[9].Code, "border-color:#000!important;");
            table = GenerateCell(table, "", "border-top:none;border-right:none;border-color:#000;");
            table = GenerateCell(table, "", "border-top:none;border-left:none;border-color:#000;");
            table = GenerateCell(table, Cells[4].Code, "border-color:#000!important;");
            table += "</tr><tr>";
            //Forth Row
            table = GenerateCell(table, Cells[8].Code, "border-color:#000!important;");
            table = GenerateCell(table, Cells[7].Code, "border-color:#000;");
            table = GenerateCell(table, Cells[6].Code, "border-color:#000;");
            table = GenerateCell(table, Cells[5].Code, "border-color:#000!important;");
            table += "</tr></tbody></table>";

            return table;
        }

        private string GenerateCell(string table, string cell, string style = "border-color:#000!important;")
        {
            table += "<td height=\"130\" width=\"130\" style=\"" + style + "\">" + cell + "</td>";
            return table;
        }

    }
}