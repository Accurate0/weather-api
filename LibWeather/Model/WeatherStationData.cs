// Generated

#nullable disable
using Newtonsoft.Json;

namespace LibWeather.Model
{

    public class Notice
    {
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("copyright_url")]
        public string CopyrightUrl { get; set; }

        [JsonProperty("disclaimer_url")]
        public string DisclaimerUrl { get; set; }

        [JsonProperty("feedback_url")]
        public string FeedbackUrl { get; set; }
    }

    public class Header
    {
        [JsonProperty("refresh_message")]
        public string RefreshMessage { get; set; }

        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("main_ID")]
        public string MainID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state_time_zone")]
        public string StateTimeZone { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class Datum
    {
        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        [JsonProperty("wmo")]
        public int Wmo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("history_product")]
        public string HistoryProduct { get; set; }

        [JsonProperty("local_date_time")]
        public string LocalDateTime { get; set; }

        [JsonProperty("local_date_time_full")]
        public string LocalDateTimeFull { get; set; }

        [JsonProperty("aifstime_utc")]
        public string AifstimeUtc { get; set; }

        [JsonProperty("lat")]
        public double? Lat { get; set; }

        [JsonProperty("lon")]
        public double? Lon { get; set; }

        [JsonProperty("apparent_t")]
        public double? ApparentT { get; set; }

        [JsonProperty("cloud")]
        public string Cloud { get; set; }

        [JsonProperty("cloud_base_m")]
        public object CloudBaseM { get; set; }

        [JsonProperty("cloud_oktas")]
        public object CloudOktas { get; set; }

        [JsonProperty("cloud_type_id")]
        public object CloudTypeId { get; set; }

        [JsonProperty("cloud_type")]
        public string CloudType { get; set; }

        [JsonProperty("delta_t")]
        public double? DeltaT { get; set; }

        [JsonProperty("gust_kmh")]
        public int? GustKmh { get; set; }

        [JsonProperty("gust_kt")]
        public int? GustKt { get; set; }

        [JsonProperty("air_temp")]
        public double? AirTemp { get; set; }

        [JsonProperty("dewpt")]
        public double? Dewpt { get; set; }

        [JsonProperty("press")]
        public double? Press { get; set; }

        [JsonProperty("press_qnh")]
        public double? PressQnh { get; set; }

        [JsonProperty("press_msl")]
        public double? PressMsl { get; set; }

        [JsonProperty("press_tend")]
        public string PressTend { get; set; }

        [JsonProperty("rain_trace")]
        public string RainTrace { get; set; }

        [JsonProperty("rel_hum")]
        public int? RelHum { get; set; }

        [JsonProperty("sea_state")]
        public string SeaState { get; set; }

        [JsonProperty("swell_dir_worded")]
        public string SwellDirWorded { get; set; }

        [JsonProperty("swell_height")]
        public object SwellHeight { get; set; }

        [JsonProperty("swell_period")]
        public object SwellPeriod { get; set; }

        [JsonProperty("vis_km")]
        public string VisKm { get; set; }

        [JsonProperty("weather")]
        public string Weather { get; set; }

        [JsonProperty("wind_dir")]
        public string WindDir { get; set; }

        [JsonProperty("wind_spd_kmh")]
        public int? WindSpdKmh { get; set; }

        [JsonProperty("wind_spd_kt")]
        public int? WindSpdKt { get; set; }
    }

    public class Observations
    {
        [JsonProperty("notice")]
        public List<Notice> Notice { get; set; }

        [JsonProperty("header")]
        public List<Header> Header { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

    public class WeatherStationData
    {
        [JsonProperty("observations")]
        public Observations Observations { get; set; }
    }
}
