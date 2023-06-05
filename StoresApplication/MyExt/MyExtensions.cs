using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StoresApplication.MyExt
{
    public static class MyExtensions
    {
        
        public static void addMaterialColumn(this DataGrid dataGrid, string header, string path)
        {
            dataGrid.Columns.Add(
                new MaterialDesignThemes.Wpf.DataGridTextColumn()
                {
                    Header = header,
                    Binding = new Binding
                    {
                        Path = new PropertyPath(path)
                    }
                }
            );
        }
        
    }
}
