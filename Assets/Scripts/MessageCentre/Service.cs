using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Service {

	//public static IServices Request = FakeService.instance;
	public static IServices Request = RealService.instance;






	//public static IServices TheService = new Service();
}