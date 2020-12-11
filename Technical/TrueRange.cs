﻿namespace ATAS.Indicators.Technical
{
	using System;
	using System.ComponentModel;

	using ATAS.Indicators.Technical.Properties;

	[DisplayName("True Range")]
	public class TrueRange : Indicator
	{
		#region Fields

		private readonly ValueDataSeries _renderSeries = new ValueDataSeries(Resources.Visualization);

		#endregion

		#region ctor

		public TrueRange()
		{
			Panel = IndicatorDataProvider.NewPanel;

			DataSeries[0] = _renderSeries;
		}

		#endregion

		#region Protected methods

		protected override void OnCalculate(int bar, decimal value)
		{
			if (bar == 0)
				return;

			var candle = GetCandle(bar);
			var prevCandle = GetCandle(bar - 1);

			var highLow = candle.High - candle.Low;
			var highCloseDiff = Math.Abs(candle.High - prevCandle.Close);
			var lowCloseDiff = Math.Abs(candle.Low - prevCandle.Close);

			var trueRange = Math.Max(highLow, highCloseDiff);
			trueRange = Math.Max(trueRange, lowCloseDiff);

			_renderSeries[bar] = trueRange;
		}

		#endregion
	}
}