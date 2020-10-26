using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Views;

namespace ApplicationCore.DataAccess
{
	public class AppDBSeed
	{
		public static async Task EnsureSeedData(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Seeding database...");

			using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var defaultContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
				defaultContext.Database.Migrate();
				
				await SeedCities(defaultContext);

				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				await SeedRoles(roleManager);
				await SeedUsers(userManager);

			}

			Console.WriteLine("Done seeding database.");
			Console.WriteLine();
		}

		

		static async Task CreateCategoryIfNotExist(DefaultContext context, Category category)
		{
			var existCategory = context.Categories.FirstOrDefault(x => x.Name == category.Name);
			if (existCategory == null)
			{
				context.Categories.Add(category);
				await context.SaveChangesAsync();
			}
		}


		static async Task SeedCities(DefaultContext context)
		{
			var cities = new List<City> 
			{ 
				new City { Title = "臺北市", Code = "A", Districts = new List<District>
					{ 
						new District { Zip = "100", Title = "中正區" },
						new District { Zip = "103", Title = "大同區" },
						new District { Zip = "104", Title = "中山區" },
						new District { Zip = "105", Title = "松山區" },
						new District { Zip = "106", Title = "大安區" },
						new District { Zip = "108", Title = "萬華區" },
						new District { Zip = "110", Title = "信義區" },
						new District { Zip = "111", Title = "士林區" },
						new District { Zip = "112", Title = "北投區" },
						new District { Zip = "114", Title = "內湖區" },
						new District { Zip = "115", Title = "南港區" },
						new District { Zip = "116", Title = "文山區" },
					} 
				},
				new City { Title = "新北市", Code = "F", Districts = new List<District>
					{ 
						new District { Zip = "207", Title = "萬里區" },
						new District { Zip = "208", Title = "金山區" },
						new District { Zip = "220", Title = "板橋區" },
						new District { Zip = "221", Title = "汐止區" },
						new District { Zip = "222", Title = "深坑區" },
						new District { Zip = "223", Title = "石碇區" },
						new District { Zip = "224", Title = "瑞芳區" },
						new District { Zip = "226", Title = "平溪區" },
						new District { Zip = "227", Title = "雙溪區" },
						new District { Zip = "228", Title = "貢寮區" },
						new District { Zip = "231", Title = "新店區" },
						new District { Zip = "232", Title = "坪林區" },
						new District { Zip = "233", Title = "烏來區" },
						new District { Zip = "234", Title = "永和區" },
						new District { Zip = "235", Title = "中和區" },
						new District { Zip = "236", Title = "土城區" },
						new District { Zip = "237", Title = "三峽區" },
						new District { Zip = "238", Title = "樹林區" },
						new District { Zip = "239", Title = "鶯歌區" },
						new District { Zip = "241", Title = "三重區" },
						new District { Zip = "242", Title = "新莊區" },
						new District { Zip = "243", Title = "泰山區" },
						new District { Zip = "244", Title = "林口區" },
						new District { Zip = "247", Title = "蘆洲區" },
						new District { Zip = "248", Title = "五股區" },
						new District { Zip = "249", Title = "八里區" },
						new District { Zip = "251", Title = "淡水區" },
						new District { Zip = "252", Title = "三芝區" },
						new District { Zip = "253", Title = "石門區" },
					} 
				},
				new City { Title = "基隆市", Code = "C", Districts = new List<District>
					{ 
						new District { Zip = "200", Title = "仁愛區" },
						new District { Zip = "201", Title = "信義區" },
						new District { Zip = "202", Title = "中正區" },
						new District { Zip = "203", Title = "中山區" },
						new District { Zip = "204", Title = "安樂區" },
						new District { Zip = "205", Title = "暖暖區" },
						new District { Zip = "206", Title = "七堵區" }
					} 
				},
				new City { Title = "宜蘭縣", Code = "G", Districts = new List<District>
					{ 
						new District { Zip = "260", Title = "宜蘭市" },
						new District { Zip = "263", Title = "壯圍鄉" },
						new District { Zip = "261", Title = "頭城鎮" },
						new District { Zip = "262", Title = "礁溪鄉" },
						new District { Zip = "264", Title = "員山鄉" },
						new District { Zip = "265", Title = "羅東鎮" },
						new District { Zip = "266", Title = "三星鄉" },
						new District { Zip = "267", Title = "大同鄉" },
						new District { Zip = "268", Title = "五結鄉" },
						new District { Zip = "269", Title = "冬山鄉" },
						new District { Zip = "270", Title = "蘇澳鎮" },
						new District { Zip = "272", Title = "南澳鄉" },
					} 
				},
				new City { Title = "桃園市", Code = "H", Districts = new List<District>
					{ 
						new District { Zip = "320", Title = "中壢區" },
						new District { Zip = "324", Title = "平鎮區" },
						new District { Zip = "325", Title = "龍潭區" },
						new District { Zip = "326", Title = "楊梅區" },
						new District { Zip = "327", Title = "新屋區" },
						new District { Zip = "328", Title = "觀音區" },
						new District { Zip = "330", Title = "桃園區" },
						new District { Zip = "333", Title = "龜山區" },
						new District { Zip = "334", Title = "八德區" },
						new District { Zip = "335", Title = "大溪區" },
						new District { Zip = "336", Title = "復興區" },
						new District { Zip = "337", Title = "大園區" },
						new District { Zip = "338", Title = "蘆竹區" },
					} 
				},
				new City { Title = "新竹市", Code = "O", Districts = new List<District>
					{ 
						new District { Zip = "300", Title = "東區" },
						new District { Zip = "300", Title = "北區" },
						new District { Zip = "300", Title = "香山區" }
					} 
				},
				new City { Title = "新竹縣", Code = "J", Districts = new List<District>
					{ 
						new District { Zip = "308", Title = "寶山鄉" },
						new District { Zip = "302", Title = "竹北市" },
						new District { Zip = "303", Title = "湖口鄉" },
						new District { Zip = "304", Title = "新豐鄉" },
						new District { Zip = "305", Title = "新埔鎮" },
						new District { Zip = "306", Title = "關西鎮" },
						new District { Zip = "307", Title = "芎林鄉" },
						new District { Zip = "310", Title = "竹東鎮" },
						new District { Zip = "311", Title = "五峰鄉" },
						new District { Zip = "312", Title = "橫山鄉" },
						new District { Zip = "313", Title = "尖石鄉" },
						new District { Zip = "314", Title = "北埔鄉" },
						new District { Zip = "315", Title = "峨眉鄉" },
					} 
				},
				new City { Title = "苗栗縣", Code = "K", Districts = new List<District>
					{ 
						new District { Zip = "350", Title = "竹南鎮" },
						new District { Zip = "351", Title = "頭份市" },
						new District { Zip = "352", Title = "三灣鄉" },
						new District { Zip = "353", Title = "南庄鄉" },
						new District { Zip = "354", Title = "獅潭鄉" },
						new District { Zip = "356", Title = "後龍鎮" },
						new District { Zip = "357", Title = "通霄鎮" },
						new District { Zip = "358", Title = "苑裡鎮" },
						new District { Zip = "360", Title = "苗栗市" },
						new District { Zip = "361", Title = "造橋鄉" },
						new District { Zip = "362", Title = "頭屋鄉" },
						new District { Zip = "363", Title = "北埔鄉" },
						new District { Zip = "364", Title = "大湖鄉" },
						new District { Zip = "365", Title = "泰安鄉" },
						new District { Zip = "366", Title = "銅鑼鄉" },
						new District { Zip = "367", Title = "三義鄉" },
						new District { Zip = "368", Title = "西湖鄉" },
						new District { Zip = "369", Title = "卓蘭鎮" }
					} 
				},
				new City { Title = "臺中市", Code = "B", Districts = new List<District>
					{ 
						new District { Zip = "400", Title = "中區" },
						new District { Zip = "401", Title = "東區" },
						new District { Zip = "402", Title = "南區" },
						new District { Zip = "403", Title = "西區" },
						new District { Zip = "404", Title = "北區" },
						new District { Zip = "406", Title = "北屯區" },
						new District { Zip = "407", Title = "西屯區" },
						new District { Zip = "408", Title = "南屯區" },
						new District { Zip = "411", Title = "太平區" },
						new District { Zip = "412", Title = "大里區" },
						new District { Zip = "413", Title = "霧峰區" },
						new District { Zip = "414", Title = "烏日區" },
						new District { Zip = "420", Title = "豐原區" },
						new District { Zip = "421", Title = "后里區" },
						new District { Zip = "422", Title = "石岡區" },
						new District { Zip = "423", Title = "東勢區" },
						new District { Zip = "424", Title = "和平區" },
						new District { Zip = "426", Title = "新社區" },
						new District { Zip = "427", Title = "潭子區" },
						new District { Zip = "428", Title = "大雅區" },						
						new District { Zip = "429", Title = "神岡區" },
						new District { Zip = "432", Title = "大肚區" },
						new District { Zip = "433", Title = "沙鹿區" },
						new District { Zip = "434", Title = "龍井區" },
						new District { Zip = "435", Title = "梧棲區" },
						new District { Zip = "436", Title = "清水區" },
						new District { Zip = "437", Title = "大甲區" },
						new District { Zip = "438", Title = "外埔區" },
						new District { Zip = "439", Title = "大安區" },
					} 
				},
				new City { Title = "彰化縣", Code = "N", Districts = new List<District>
					{ 
						new District { Zip = "500", Title = "彰化市" },
						new District { Zip = "502", Title = "芬園鄉" },
						new District { Zip = "503", Title = "花壇鄉" },
						new District { Zip = "504", Title = "秀水鄉" },
						new District { Zip = "505", Title = "鹿港鎮" },
						new District { Zip = "506", Title = "福興鄉" },
						new District { Zip = "507", Title = "線西鄉" },
						new District { Zip = "508", Title = "和美鎮" },
						new District { Zip = "509", Title = "伸港鄉" },
						new District { Zip = "510", Title = "員林市" },
						new District { Zip = "511", Title = "社頭鄉" },
						new District { Zip = "512", Title = "永靖鄉" },
						new District { Zip = "513", Title = "埔心鄉" },
						new District { Zip = "514", Title = "溪湖鎮" },
						new District { Zip = "515", Title = "大村鄉" },
						new District { Zip = "516", Title = "埔鹽鄉" },
						new District { Zip = "520", Title = "田中鎮" },
						new District { Zip = "521", Title = "北斗鎮" },
						new District { Zip = "522", Title = "田尾鄉" },
						new District { Zip = "523", Title = "埤頭鄉" },
						new District { Zip = "524", Title = "溪州鄉" },
						new District { Zip = "525", Title = "竹塘鄉" },
						new District { Zip = "526", Title = "二林鎮" },
						new District { Zip = "527", Title = "大城鄉" },
						new District { Zip = "528", Title = "芳苑鄉" },
						new District { Zip = "530", Title = "二水鄉" }
					} 
				},

				new City { Title = "南投縣", Code = "M", Districts = new List<District>
					{ 
						new District { Zip = "540", Title = "南投市" },
						new District { Zip = "541", Title = "中寮鄉" },
						new District { Zip = "542", Title = "草屯鎮" },
						new District { Zip = "544", Title = "國姓鄉" },
						new District { Zip = "545", Title = "埔里鎮" },
						new District { Zip = "546", Title = "仁愛鄉" },
						new District { Zip = "551", Title = "名間鄉" },
						new District { Zip = "552", Title = "集集鎮" },
						new District { Zip = "553", Title = "水里鄉" },
						new District { Zip = "555", Title = "魚池鄉" },
						new District { Zip = "556", Title = "信義鄉" },
						new District { Zip = "557", Title = "竹山鎮" },
						new District { Zip = "558", Title = "鹿谷鄉" }						
						
					} 
				},

				new City { Title = "嘉義市", Code = "I", Districts = new List<District>
					{ 
						new District { Zip = "600", Title = "西區" },
						new District { Zip = "600", Title = "東區" }					
						
					} 
				},

				new City { Title = "嘉義縣", Code = "Q", Districts = new List<District>
					{ 
						new District { Zip = "602", Title = "番路鄉" },
						new District { Zip = "603", Title = "梅山鄉" },
						new District { Zip = "604", Title = "竹崎鄉" },
						new District { Zip = "605", Title = "阿里山鄉" },
						new District { Zip = "606", Title = "中埔鄉" },
						new District { Zip = "607", Title = "大埔鄉" },
						new District { Zip = "608", Title = "水上鄉" },
						new District { Zip = "611", Title = "鹿草鄉" },
						new District { Zip = "612", Title = "太保市" },
						new District { Zip = "613", Title = "朴子市" },
						new District { Zip = "614", Title = "東石鄉" },
						new District { Zip = "615", Title = "六腳鄉" },
						new District { Zip = "616", Title = "新港鄉" },
						new District { Zip = "621", Title = "民雄鄉" },
						new District { Zip = "622", Title = "大林鎮" },
						new District { Zip = "623", Title = "溪口鄉" },
						new District { Zip = "624", Title = "義竹鄉" },
						new District { Zip = "625", Title = "布袋鎮" }						
						
					} 
				},

				new City { Title = "雲林縣", Code = "P", Districts = new List<District>
					{ 
						new District { Zip = "630", Title = "斗南鎮" },
						new District { Zip = "631", Title = "大埤鄉" },
						new District { Zip = "632", Title = "虎尾鎮" },
						new District { Zip = "633", Title = "土庫鎮" },
						new District { Zip = "634", Title = "褒忠鄉" },
						new District { Zip = "635", Title = "東勢鄉" },
						new District { Zip = "636", Title = "臺西鄉" },
						new District { Zip = "637", Title = "崙背鄉" },
						new District { Zip = "638", Title = "麥寮鄉" },
						new District { Zip = "640", Title = "斗六市" },
						new District { Zip = "643", Title = "林內鄉" },
						new District { Zip = "646", Title = "古坑鄉" },
						new District { Zip = "647", Title = "莿桐鄉" },
						new District { Zip = "648", Title = "西螺鎮" },
						new District { Zip = "649", Title = "二崙鄉" },
						new District { Zip = "651", Title = "北港鎮" },
						new District { Zip = "652", Title = "水林鄉" },
						new District { Zip = "653", Title = "口湖鄉" },
						new District { Zip = "654", Title = "四湖鄉" },
						new District { Zip = "655", Title = "元長鄉" }						
						
					} 
				},

				new City { Title = "臺南市", Code = "D", Districts = new List<District>
					{ 
						new District { Zip = "700", Title = "中西區" },
						new District { Zip = "701", Title = "東區" },
						new District { Zip = "702", Title = "南區" },
						new District { Zip = "704", Title = "北區" },
						new District { Zip = "708", Title = "安平區" },
						new District { Zip = "709", Title = "安南區" },
						new District { Zip = "710", Title = "永康區" },
						new District { Zip = "712", Title = "新化區" },
						new District { Zip = "713", Title = "左鎮區" },
						new District { Zip = "714", Title = "玉井區" },
						new District { Zip = "715", Title = "楠西區" },
						new District { Zip = "716", Title = "南化區" },
						new District { Zip = "717", Title = "仁德區" },
						new District { Zip = "718", Title = "關廟區" },
						new District { Zip = "719", Title = "龍崎區" },
						new District { Zip = "720", Title = "官田區" },
						new District { Zip = "721", Title = "麻豆區" },
						new District { Zip = "722", Title = "佳里區" },
						new District { Zip = "723", Title = "西港區" },
						new District { Zip = "724", Title = "七股區" },
						new District { Zip = "725", Title = "將軍區" },
						new District { Zip = "726", Title = "學甲區" },
						new District { Zip = "727", Title = "北門區" },
						new District { Zip = "730", Title = "新營區" },
						new District { Zip = "731", Title = "後壁區" },
						new District { Zip = "732", Title = "白河區" },
						new District { Zip = "733", Title = "東山區" },
						new District { Zip = "734", Title = "六甲區" },
						new District { Zip = "735", Title = "下營區" },
						new District { Zip = "736", Title = "柳營區" },
						new District { Zip = "737", Title = "鹽水區" },
						new District { Zip = "741", Title = "善化區" },
						new District { Zip = "744", Title = "新市區" },
						new District { Zip = "742", Title = "大內區" },
						new District { Zip = "743", Title = "山上區" },
						new District { Zip = "745", Title = "安定區" }				
						
					} 
				},

				new City { Title = "高雄市", Code = "E", Districts = new List<District>
					{ 
						new District { Zip = "800", Title = "新興區" },
						new District { Zip = "801", Title = "前金區" },
						new District { Zip = "802", Title = "苓雅區" },
						new District { Zip = "803", Title = "鹽埕區" },
						new District { Zip = "804", Title = "鼓山區" },
						new District { Zip = "805", Title = "旗津區" },
						new District { Zip = "806", Title = "前鎮區" },
						new District { Zip = "807", Title = "三民區" },
						new District { Zip = "811", Title = "楠梓區" },
						new District { Zip = "812", Title = "小港區" },
						new District { Zip = "813", Title = "左營區" },
						new District { Zip = "814", Title = "仁武區" },
						new District { Zip = "815", Title = "大社區" },
						new District { Zip = "820", Title = "岡山區" },
						new District { Zip = "821", Title = "路竹區" },
						new District { Zip = "822", Title = "阿蓮區" },
						new District { Zip = "823", Title = "田寮區" },
						new District { Zip = "824", Title = "燕巢區" },
						new District { Zip = "825", Title = "橋頭區" },
						new District { Zip = "826", Title = "梓官區" },
						new District { Zip = "827", Title = "彌陀區" },
						new District { Zip = "828", Title = "永安區" },
						new District { Zip = "829", Title = "湖內區" },
						new District { Zip = "830", Title = "鳳山區" },
						new District { Zip = "831", Title = "大寮區" },
						new District { Zip = "832", Title = "林園區" },
						new District { Zip = "833", Title = "鳥松區" },
						new District { Zip = "840", Title = "大樹區" },
						new District { Zip = "842", Title = "旗山區" },
						new District { Zip = "843", Title = "美濃區" },
						new District { Zip = "844", Title = "六龜區" },
						new District { Zip = "845", Title = "內門區" },
						new District { Zip = "846", Title = "杉林區" },
						new District { Zip = "847", Title = "甲仙區" },
						new District { Zip = "848", Title = "桃源區" },
						new District { Zip = "849", Title = "那瑪夏區" },
						new District { Zip = "851", Title = "茂林區" },
						new District { Zip = "852", Title = "茄萣區" }			
						
					} 
				},

				new City { Title = "澎湖縣", Code = "X", Districts = new List<District>
					{ 
						new District { Zip = "880", Title = "馬公市" },
						new District { Zip = "881", Title = "西嶼鄉" },
						new District { Zip = "882", Title = "望安鄉" },
						new District { Zip = "883", Title = "七美鄉" },
						new District { Zip = "884", Title = "白沙鄉" },
						new District { Zip = "885", Title = "湖西鄉" }			
						
					} 
				},

				

				new City { Title = "屏東縣", Code = "T", Districts = new List<District>
					{ 
						new District { Zip = "900", Title = "屏東市" },
						new District { Zip = "901", Title = "三地門鄉" },
						new District { Zip = "902", Title = "霧臺鄉" },
						new District { Zip = "903", Title = "瑪家鄉" },
						new District { Zip = "904", Title = "九如鄉" },
						new District { Zip = "905", Title = "里港鄉" },
						new District { Zip = "906", Title = "高樹鄉" },
						new District { Zip = "907", Title = "鹽埔鄉" },
						new District { Zip = "908", Title = "長治鄉" },
						new District { Zip = "909", Title = "麟洛鄉" },
						new District { Zip = "911", Title = "竹田鄉" },
						new District { Zip = "912", Title = "內埔鄉" },
						new District { Zip = "913", Title = "萬丹鄉" },
						new District { Zip = "920", Title = "潮州鎮" },
						new District { Zip = "921", Title = "泰武鄉" },
						new District { Zip = "922", Title = "來義鄉" },
						new District { Zip = "923", Title = "萬巒鄉" },
						new District { Zip = "924", Title = "崁頂鄉" },
						new District { Zip = "925", Title = "新埤鄉" },
						new District { Zip = "926", Title = "南州鄉" },
						new District { Zip = "927", Title = "林邊鄉" },
						new District { Zip = "928", Title = "東港鎮" },
						new District { Zip = "929", Title = "琉球鄉" },
						new District { Zip = "931", Title = "佳冬鄉" },
						new District { Zip = "932", Title = "新園鄉" },
						new District { Zip = "940", Title = "枋寮鄉" },
						new District { Zip = "941", Title = "枋山鄉" },
						new District { Zip = "942", Title = "春日鄉" },
						new District { Zip = "943", Title = "獅子鄉" },
						new District { Zip = "944", Title = "車城鄉" },
						new District { Zip = "945", Title = "牡丹鄉" },
						new District { Zip = "946", Title = "恆春鎮" },
						new District { Zip = "947", Title = "滿州鄉" }	
						
					} 
				},

				new City { Title = "臺東縣", Code = "V", Districts = new List<District>
					{ 
						new District { Zip = "950", Title = "臺東市" },
						new District { Zip = "951", Title = "綠島鄉" },
						new District { Zip = "952", Title = "蘭嶼鄉" },
						new District { Zip = "953", Title = "延平鄉" },
						new District { Zip = "954", Title = "卑南鄉" },
						new District { Zip = "955", Title = "鹿野鄉" },
						new District { Zip = "956", Title = "關山鎮" },
						new District { Zip = "957", Title = "海端鄉" },
						new District { Zip = "958", Title = "池上鄉" },
						new District { Zip = "959", Title = "東河鄉" },
						new District { Zip = "961", Title = "成功鎮" },
						new District { Zip = "962", Title = "長濱鄉" },
						new District { Zip = "963", Title = "太麻里鄉" },
						new District { Zip = "964", Title = "金峰鄉" },
						new District { Zip = "965", Title = "大武鄉" },
						new District { Zip = "966", Title = "達仁鄉" }					
						
					} 
				},

				new City { Title = "花蓮縣", Code = "U", Districts = new List<District>
					{ 
						new District { Zip = "970", Title = "花蓮市" },
						new District { Zip = "971", Title = "新城鄉" },
						new District { Zip = "972", Title = "秀林鄉" },
						new District { Zip = "973", Title = "吉安鄉" },
						new District { Zip = "974", Title = "壽豐鄉" },
						new District { Zip = "975", Title = "鳳林鎮" },
						new District { Zip = "976", Title = "光復鄉" },
						new District { Zip = "977", Title = "豐濱鄉" },
						new District { Zip = "978", Title = "瑞穗鄉" },
						new District { Zip = "979", Title = "萬榮鄉" },
						new District { Zip = "981", Title = "玉里鎮" },
						new District { Zip = "982", Title = "卓溪鄉" },
						new District { Zip = "983", Title = "富里鄉" }			
						
					} 
				},

				new City { Title = "金門縣", Code = "W", Districts = new List<District>
					{ 
						new District { Zip = "890", Title = "金沙鎮" },
						new District { Zip = "891", Title = "金湖鎮" },
						new District { Zip = "892", Title = "金寧鄉" },
						new District { Zip = "893", Title = "金城鎮" },
						new District { Zip = "894", Title = "烈嶼鄉" },
						new District { Zip = "896", Title = "烏坵鄉" }			
						
					} 
				},

				new City { Title = "連江縣", Code = "Z", Districts = new List<District>
					{ 
						new District { Zip = "209", Title = "南竿鄉" },
						new District { Zip = "210", Title = "北竿鄉" },
						new District { Zip = "211", Title = "莒光鄉" },
						new District { Zip = "212", Title = "東引鄉" }		
						
					} 
				},
			};
			foreach (var item in cities)
			{
				await CreateCityIfNotExist(context, item);
			}


		}

		static async Task CreateCityIfNotExist(DefaultContext context, City city)
		{
			var existCity = context.Cities.FirstOrDefault(x => x.Code == city.Code);
			if (existCity == null)
			{
				context.Cities.Add(city);
				await context.SaveChangesAsync();
			}
		}

		static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
		{
			var roles = new List<string> { "Dev", "Boss", "Subscriber" };
			foreach (var item in roles)
			{
				await AddRoleIfNotExist(roleManager, item);
			}


		}

		static async Task AddRoleIfNotExist(RoleManager<IdentityRole> roleManager, string roleName)
		{
			var role = await roleManager.FindByNameAsync(roleName);
			if (role == null)
			{
				await roleManager.CreateAsync(new IdentityRole { Name = roleName });

			}


		}

		static async Task SeedUsers(UserManager<User> userManager)
		{
			string email = "traders.com.tw@gmail.com";
			var roles = new List<string>() { "Dev" };

			await CreateUserIfNotExist(userManager, email, roles);

			

		}


		static async Task CreateUserIfNotExist(UserManager<User> userManager, string email, IList<string> roles = null)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				bool isAdmin = false;
				if (!roles.IsNullOrEmpty())
				{
					isAdmin = roles.Select(r => r == "Dev" || r == "Boss").FirstOrDefault();
				}

				var newUser = new User
				{
					Email = email,
					UserName = email,


					EmailConfirmed = isAdmin,
					SecurityStamp = Guid.NewGuid().ToString()

				};


				var result = await userManager.CreateAsync(newUser);

				if (!roles.IsNullOrEmpty())
				{
					await userManager.AddToRolesAsync(newUser, roles);
				}


			}
			else
			{
				if (!roles.IsNullOrEmpty())
				{
					foreach (var role in roles)
					{
						bool hasRole = await userManager.IsInRoleAsync(user, role);
						if (!hasRole) await userManager.AddToRoleAsync(user, role);
					}
				}

			}
		}

		

	}
}
