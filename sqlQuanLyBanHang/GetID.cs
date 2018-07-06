using System;
using System.Linq;

namespace sqlQuanLiBanHang
{
	public class GetID
	{
		private BH db;
		private DateTime currentDate;
		private string strCurrentDate;

		public GetID()
		{
			db = new BH();
			currentDate = DateTime.Today;
			strCurrentDate = convertDateToString();
		}

		private string convertDateToString()
		{
			return currentDate.Year.ToString() + currentDate.Month.ToString("00") + currentDate.Day.ToString("00");
		}

		public string GetNewPurchaseOrder()
		{
			var id = db.PurchaseOrders.Where(e => e.OrderNO.Contains("P"+ strCurrentDate)).OrderByDescending(e => e.OrderNO).ToList();

			if (id.Count == 0)
				return "P" + strCurrentDate + "001";
			else
			{
				int newID = Convert.ToInt32(id[0].OrderNO.Substring(9,3)) + 1;
				return "P" + strCurrentDate + newID.ToString("000");
			}
		}

		public string GetNewSalesOrder()
		{
			var id = db.SalesOrders.Where(e => e.OrderNo.Contains("S" + strCurrentDate)).OrderByDescending(e => e.OrderNo).ToList();

			if (id.Count == 0)
				return "S" + strCurrentDate + "001";
			else
			{
				int newID = Convert.ToInt32(id[0].OrderNo.Substring(9, 3)) + 1;
				return "S" + strCurrentDate + newID.ToString("000");
			}
		}
	}
}
