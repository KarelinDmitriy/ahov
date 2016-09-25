namespace Forcast
{
	public class ActiveData
	{
		/// <summary>
		/// �������� �����
		/// </summary>
		public double U { get; set; }
		/// <summary>
		/// ����������� �������
		/// </summary>
		public double Tcw { get; set; }

		/// <summary>
		/// ����� ����� ������
		/// </summary>
		public double T { get; set; }

		/// <summary>
		/// ����� ���������� ��������� 
		/// </summary>
		public double[] Tn { get; set; }

		/// <summary>
		/// ��������� ����� ������� ����������� ������ � ���� ��������������� �����
		/// </summary>
		public double q { set; get; }

		/// <summary>
		/// ������������ ������������ �������
		/// </summary>
		public Table_3_3 AirVerticalStable { get; set; }

		public double Ku9 { get; set; } //��� ��� ��������? 

		public double Ku2 { get; set; }
	}
}