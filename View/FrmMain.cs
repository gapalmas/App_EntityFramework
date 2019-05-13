using System;
using System.Collections.Generic;
using System.Windows.Forms;
using _producto = Controller.GenericController<Model.productos>;


namespace View
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnProcesar_Click(object sender, EventArgs e)
        {
            var Productos = new List<Model.productos>();
            //Metodo CREATE
            var producto1 = new Model.productos
            {
                nombre = "PS4",
                fecha = DateTime.Now.AddDays(10),
                estatus = 1 /*True*/
            };

            var producto2 = new Model.productos
            {
                nombre = "XBOX ONE",
                fecha = DateTime.Now.AddDays(10),
                estatus = 1 /*True*/
            };

            Productos.Add(producto1);
            Productos.Add(producto2);

            _producto.Create(producto1);

            _producto.AddRange(Productos);

            //Metodo READ
            foreach (var item in _producto.Read())
            {
                MessageBox.Show("Este producto tiene identificador :" + item.id_productos);
            }

            


            //Metodo  UPDATE
            foreach (var item in _producto.Read())
            {
                item.nombre = "UPDATE";
                item.fecha = DateTime.Now;

                _producto.Update(item, item.id_productos);
            }


            //Metodo DELETE
            //
            //Borrar rango de registros
            _producto.DeleteRange(_producto.Read());


            _producto.AddRange(Productos);

            dgv.DataSource = _producto.Read();
        }
    }
}