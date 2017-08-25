using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://mapzen.com/documentation/mobility/turn-by-turn/api-reference/#outputs-of-a-route

namespace MapzenJson
{
	[System.Serializable]
	public class MapZenResponse
	{
		public string id;
		public Trip trip;
	}

	[System.Serializable]
	public class Trip
	{
		public string language;
		public Summary summary;
		public List<MapZenLocation> locations;
		public string units;
		public List<LegsItem> legs;
		public string status_message;
		public int status;
	}

	[System.Serializable]
	public class Summary
	{
		public double max_lon;
		public double max_lat;
		public int time;
		public double length;
		public double min_lat;
		public double min_lon;
	}

	[System.Serializable]
	public class MapZenLocation
	{
		public string street;
		public double lon;
		public double lat;
		public string type;

		/*public string heading;
		public string heading_tolerance;
		public string way_id;
		public string minimum_reachability;
		public string radius;
		public string name;
		public string city;
		public string state;
		public string postal_code;
		public string country;
		public string phone;
		public string url;*/

		public string side_of_street;
		public string date_time;
	}

	[System.Serializable]
	public class ManeuverItem
	{
		public int type;
		public string instruction;
		public string verbal_transition_alert_instruction;
		public string verbal_pre_transition_instruction;
		public string verbal_post_transition_instruction;
		public List<string> street_names;
		public List<string> begin_street_names;
		public int time;
		public double length;
		public int begin_shape_index;
		public int end_shape_index;
		public bool toll;
		public bool rough;
		public bool gate;
		public bool ferry;
		public Sign sign;
		public int roundabout_exit_count; //??
		public string depart_instruction; //??
		public string verbal_depart_instruction; //?:
		public string arrive_instruction; //?:
		public string verbal_arrive_instruction; //??
		public string transit_info; //??
		public bool verbal_multi_cue;
		public string travel_mode;
		public string travel_type;
	}

	[System.Serializable]
	public class LegsItem
	{
		public string shape;
		public Summary summary;
		public List<ManeuverItem> maneuvers;
	}

	[System.Serializable]
	public class Sign
	{
		public List<string> exit_number_elements;
		public List<string> exit_branch_elements;
		public List<string> exit_toward_elements;
		public List<string> exit_name_elements;
	}

	[System.Serializable]
	public class ManeuverTransitItem
	{
		public string onestop_id; //??
		public string short_name;
		public string long_name;
		public string headsign;
		public int color;
		public int text_color;
		public string description;
		public string operator_onestop_id; //??
		public string operator_name;
		public string operator_url;
		public List<TransitStopItem> transit_stops;
	}

	[System.Serializable]
	public class TransitStopItem
	{
		public int type;
		public string onestop_id; //??
		public string name;
		public string arrival_date_time;
		public string departure_date_time;
		public bool is_parent_stop;
		public bool assumed_schedule;
		public double lat;
		public double lon;
	}

	[System.Serializable]
	public class ManeuverSignElementItem
	{
		public string text;
		public int consecutive_count; //??
	}
}
