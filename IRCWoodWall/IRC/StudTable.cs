﻿using Bim.Domain.Ifc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bim.Application.IRCWood.IRC
{
    public class StudTable
    {
        public string[] Spacing { get; set; }
        public string[] Keys { get; set; }
        public int Floors { get; set; }
        public List<StudCell> Cells { get; set; }
        public StudTable()
        {
            Cells = new List<StudCell>();
            Floors = 3;
        }



        public List<StudCell> GetSpace(int floor, double height, IfDimension dimension)
        {

            return Cells.Where(e =>
             {

                 return e.Floor == floor && e.Height >= height &&
                e.Value.XDim == dimension.XDim && e.Value.YDim == dimension.YDim;

             }).ToList();

        }

        public static StudTable Load(string filePath)
        {
            var table = new StudTable();
            string[] data = File.ReadAllLines(filePath);
            table.Spacing = data[0].Split(',');
            table.Keys = data[1].Split(',');

            string[] values = null;
            for (int k = 0; k < table.Floors; k++)
            {
                switch (k)
                {
                    case 0:
                        values = data.Skip(2).ToArray();
                        break;
                    case 1:
                        values = data.Skip(10).ToArray();
                        break;
                    case 2:
                        values = data.Skip(18).ToArray();
                        break;

                    default:
                        break;
                }

                for (int i = 0; i < table.Keys.Length; i++)
                {
                    for (int j = 0; j < table.Spacing.Length; j++)
                    {
                        string[] dimRaw = values[i].Split(',');
                        var dim = dimRaw[j].Split(' ');

                        var x = Convert.ToDouble(dim[0]);
                        var y = Convert.ToDouble(dim[1]);
                        var cell = new StudCell(
                           Convert.ToDouble(table.Spacing[j]),
                            Convert.ToDouble(table.Keys[i]),
                            new IfDimension(x, y, 0)
                            );

                        cell.Floor = k + 1;
                        table.Cells.Add(cell);
                    }
                }

            }


            return table;


        }


    }
}
