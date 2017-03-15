using System.ComponentModel;
using AhovRepository.Entity;
using Forcast;

namespace Web.Models.Forcast
{
	public class ActiveModel
	{
		public int OrgId { get; set; }

		[DisplayName("Скорость ветра")]
		public double WildSpeed { get; set; }

		[DisplayName("Температура воздуха")]
		public double Temperature { get; set; }

		[DisplayName("Время после аварии")]
		public double T { get; set; }

		[DisplayName("Время оповещения населения")]
		public double Tn { get; set; }

		[DisplayName("Время оповещения персонала")]
		public double To { get; set; }

		[DisplayName("Рассояние между центром пораженного объета и осью распространения ветра")]
		public double Q { get; set; }

		[DisplayName("Состояние воздуха")]
		public StateType StateType { get; set; }
	}
}