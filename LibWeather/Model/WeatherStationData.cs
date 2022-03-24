// Generated

#nullable disable
using Newtonsoft.Json;

namespace LibWeather.Model
{

    public class Notice
    {
        [JsonProperty("copyright", NullValueHandling = NullValueHandling.Ignore)]
        public string Copyright { get; set; }

        [JsonProperty("copyright_url", NullValueHandling = NullValueHandling.Ignore)]
        public string CopyrightUrl { get; set; }

        [JsonProperty("disclaimer_url", NullValueHandling = NullValueHandling.Ignore)]
        public string DisclaimerUrl { get; set; }

        [JsonProperty("feedback_url", NullValueHandling = NullValueHandling.Ignore)]
        public string FeedbackUrl { get; set; }
    }

    public class Header
    {
        [JsonProperty("refresh_message", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshMessage { get; set; }

        [JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        [JsonProperty("main_ID", NullValueHandling = NullValueHandling.Ignore)]
        public string MainID { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("state_time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string StateTimeZone { get; set; }

        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("product_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
    }

    public class Datum
    {
        [JsonProperty("sort_order", NullValueHandling = NullValueHandling.Ignore)]
        public int SortOrder { get; set; }

        [JsonProperty("wmo", NullValueHandling = NullValueHandling.Ignore)]
        public int Wmo { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("history_product", NullValueHandling = NullValueHandling.Ignore)]
        public string HistoryProduct { get; set; }

        [JsonProperty("local_date_time", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalDateTime { get; set; }

        [JsonProperty("local_date_time_full", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalDateTimeFull { get; set; }

        [JsonProperty("aifstime_utc", NullValueHandling = NullValueHandling.Ignore)]
        public string AifstimeUtc { get; set; }

        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public double Lat { get; set; }

        [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
        public double Lon { get; set; }

        [JsonProperty("apparent_t", NullValueHandling = NullValueHandling.Ignore)]
        public double ApparentT { get; set; }

        [JsonProperty("cloud", NullValueHandling = NullValueHandling.Ignore)]
        public string Cloud { get; set; }

        [JsonProperty("cloud_base_m", NullValueHandling = NullValueHandling.Ignore)]
        public object CloudBaseM { get; set; }

        [JsonProperty("cloud_oktas", NullValueHandling = NullValueHandling.Ignore)]
        public object CloudOktas { get; set; }

        [JsonProperty("cloud_type_id", NullValueHandling = NullValueHandling.Ignore)]
        public object CloudTypeId { get; set; }

        [JsonProperty("cloud_type", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudType { get; set; }

        [JsonProperty("delta_t", NullValueHandling = NullValueHandling.Ignore)]
        public double DeltaT { get; set; }

        [JsonProperty("gust_kmh", NullValueHandling = NullValueHandling.Ignore)]
        public int GustKmh { get; set; }

        [JsonProperty("gust_kt", NullValueHandling = NullValueHandling.Ignore)]
        public int GustKt { get; set; }

        [JsonProperty("air_temp", NullValueHandling = NullValueHandling.Ignore)]
        public double AirTemp { get; set; }

        [JsonProperty("dewpt", NullValueHandling = NullValueHandling.Ignore)]
        public double Dewpt { get; set; }

        [JsonProperty("press", NullValueHandling = NullValueHandling.Ignore)]
        public double Press { get; set; }

        [JsonProperty("press_qnh", NullValueHandling = NullValueHandling.Ignore)]
        public double PressQnh { get; set; }

        [JsonProperty("press_msl", NullValueHandling = NullValueHandling.Ignore)]
        public double PressMsl { get; set; }

        [JsonProperty("press_tend", NullValueHandling = NullValueHandling.Ignore)]
        public string PressTend { get; set; }

        [JsonProperty("rain_trace", NullValueHandling = NullValueHandling.Ignore)]
        public string RainTrace { get; set; }

        [JsonProperty("rel_hum", NullValueHandling = NullValueHandling.Ignore)]
        public int RelHum { get; set; }

        [JsonProperty("sea_state", NullValueHandling = NullValueHandling.Ignore)]
        public string SeaState { get; set; }

        [JsonProperty("swell_dir_worded", NullValueHandling = NullValueHandling.Ignore)]
        public string SwellDirWorded { get; set; }

        [JsonProperty("swell_height", NullValueHandling = NullValueHandling.Ignore)]
        public object SwellHeight { get; set; }

        [JsonProperty("swell_period", NullValueHandling = NullValueHandling.Ignore)]
        public object SwellPeriod { get; set; }

        [JsonProperty("vis_km", NullValueHandling = NullValueHandling.Ignore)]
        public string VisKm { get; set; }

        [JsonProperty("weather", NullValueHandling = NullValueHandling.Ignore)]
        public string Weather { get; set; }

        [JsonProperty("wind_dir", NullValueHandling = NullValueHandling.Ignore)]
        public string WindDir { get; set; }

        [JsonProperty("wind_spd_kmh", NullValueHandling = NullValueHandling.Ignore)]
        public int WindSpdKmh { get; set; }

        [JsonProperty("wind_spd_kt", NullValueHandling = NullValueHandling.Ignore)]
        public int WindSpdKt { get; set; }
    }

    public class Observations
    {
        [JsonProperty("notice", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notice> Notice { get; set; }

        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public List<Header> Header { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Datum> Data { get; set; }
    }

    public class WeatherStationData
    {
        [JsonProperty("observations", NullValueHandling = NullValueHandling.Ignore)]
        public Observations Observations { get; set; }
    }
}
