# Prophecy 中文玄学综合工具

[![NuGet](https://img.shields.io/nuget/v/Prophecy.svg?style=flat-square)](https://www.nuget.org/packages/Prophecy)

Prophecy is a library based on ​**Sxwnl (寿星万年历)**, providing various mystical calculation utilities...

 - 基础版本来自于[SharpSxwnl](https://github.com/HongchenMeng/SharpSxwnl)


## Features

- ​[x] 历法转换 Calendar Conversions:
  - 公历 Julian calendar
  - 农历 Chinese lunar calendar
  - 回历 Islamic calendar
- [x] 天文学工具 朔望日计算
- [ ] 四柱（八字）摆盘
- [ ] 星座推算
- [ ] 奇门遁甲演局
- [ ] 大六壬演局
- [ ] ......

## Installation

You can install the library via NuGet Package Manager:

```bash
dotnet add package Prophecy

```


## Usage


```csharp

using Prophecy;

JDateTime jdt = new JDateTime(DateTime.Now);

Console.WriteLine($"儒略日：{jdt.JulianDate}   {jdt.JulianDateFrom2000}");
Console.WriteLine($"公历：{jdt.ToStringGeroge("yyyy年MM月dd日 星期W HH:mm:ss")}");
Console.WriteLine($"回历：{jdt.ToStringIslamic("yyyy年MM月dd日")}");
Console.WriteLine($"农历：{jdt.ToStringLunar("yyyy年MM月dd日 H时m刻")}");
Console.WriteLine($"农历：{jdt.LunarFourPillars.Year.ToString()}{jdt.LunarShengxiao.ToString()}年 {(jdt.IsLunarLeapMonth ? "闰" : "")}{jdt.LunarMonthName}月{jdt.LunarDayName}日 ({(jdt.IsLunarBigMonth ? "大" : "小")}) {jdt.LunarShiKe} {jdt.Jieqi.ToString()}已过{jdt.JieqiBegin:N2}天{(jdt.isTodayJieqi ? "★" : "")}");

var r = jdt.LunarFourPillars;
Console.WriteLine($"四柱（农历）：{r.Year} {r.Month} {r.Day} {r.Hour}");
var r0 = jdt.LunarFourPillars0;
Console.WriteLine($"四柱（节气）：{r0.Year} {r0.Month} {r0.Day} {r0.Hour}");

Console.WriteLine($"{jdt.FeastInfo}");
Console.WriteLine($"{Prophecy.Data.ChaodaiInfo.getChaodaiDesc(jdt)}");
foreach (var (date, feast) in jdt.FeastComing())
{
    Console.WriteLine($"再过{(int)(date.JulianDate0 - jdt.JulianDate + 1)}天：{feast}");
}

```