/*
Copyright 2022 Smyrilline

Use of this source code is governed by an MIT-style
license that can be found in the LICENSE file or at
https://opensource.org/licenses/MIT.

Author: Tummas Andreasen
*/
using System;
using System.Linq;
using System.Text;

namespace FaroeseTelecom
{
    public static class PhoneChecker
	{
		private static readonly CountryCode[] CountryPrefixList = new CountryCode[]
		{
			new CountryCode(CountryShort: "US/CA", Prefix: "+1",    PrefixValue: 1,    Country: "US (+1) / Canada (+1)"                   , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "RU/KZ", Prefix: "+7",    PrefixValue: 7,    Country: "Russia (+7) / Kazakhstan (+7)"           , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "EG",    Prefix: "+20",   PrefixValue: 20,   Country: "Egypt (+20)"                             , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "ZA",    Prefix: "+27",   PrefixValue: 27,   Country: "South Africa (+27)"                      , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GR",    Prefix: "+30",   PrefixValue: 30,   Country: "Greece (+30)"                            , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "NL",    Prefix: "+31",   PrefixValue: 31,   Country: "Netherlands (+31)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "BE",    Prefix: "+32",   PrefixValue: 32,   Country: "Belgium (+32)"                           , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "FR",    Prefix: "+33",   PrefixValue: 33,   Country: "France (+33)"                            , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "ES",    Prefix: "+34",   PrefixValue: 34,   Country: "Spain (+34)"                             , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "HU",    Prefix: "+36",   PrefixValue: 36,   Country: "Hungary (+36)"                           , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "IT/VA", Prefix: "+39",   PrefixValue: 39,   Country: "Italy (+39) / Vatican City (+39)"        , NationalLengthMin: 3,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "RO",    Prefix: "+40",   PrefixValue: 40,   Country: "Romania (+40)"                           , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "CH",    Prefix: "+41",   PrefixValue: 41,   Country: "Switzerland (+41)"                       , NationalLengthMin: 4,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "AT",    Prefix: "+43",   PrefixValue: 43,   Country: "Austria (+43)"                           , NationalLengthMin: 4,  NationalLengthMax: 13, Locked: true),
			new CountryCode(CountryShort: "GB",    Prefix: "+44",   PrefixValue: 44,   Country: "United Kingdom (+44)"                    , NationalLengthMin: 7,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "DK",    Prefix: "+45",   PrefixValue: 45,   Country: "Denmark (+45)"                           , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "SE",    Prefix: "+46",   PrefixValue: 46,   Country: "Sweden (+46)"                            , NationalLengthMin: 7,  NationalLengthMax: 13, Locked: true),
			new CountryCode(CountryShort: "NO",    Prefix: "+47",   PrefixValue: 47,   Country: "Norway (+47)"                            , NationalLengthMin: 5,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "PL",    Prefix: "+48",   PrefixValue: 48,   Country: "Poland (+48)"                            , NationalLengthMin: 6,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "DE",    Prefix: "+49",   PrefixValue: 49,   Country: "Germany (+49)"                           , NationalLengthMin: 6,  NationalLengthMax: 13, Locked: true),
			new CountryCode(CountryShort: "PE",    Prefix: "+51",   PrefixValue: 51,   Country: "Peru (+51)"                              , NationalLengthMin: 8,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "MX",    Prefix: "+52",   PrefixValue: 52,   Country: "Mexico (+52)"                            , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "CU",    Prefix: "+53",   PrefixValue: 53,   Country: "Cuba (+53)"                              , NationalLengthMin: 6,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "AR",    Prefix: "+54",   PrefixValue: 54,   Country: "Argentina (+54)"                         , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "BR",    Prefix: "+55",   PrefixValue: 55,   Country: "Brazil (+55)"                            , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "CL",    Prefix: "+56",   PrefixValue: 56,   Country: "Chile (+56)"                             , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "CO",    Prefix: "+57",   PrefixValue: 57,   Country: "Colombia (+57)"                          , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "VE",    Prefix: "+58",   PrefixValue: 58,   Country: "Venezuela (+58)"                         , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "MY",    Prefix: "+60",   PrefixValue: 60,   Country: "Malaysia (+60)"                          , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "AU",    Prefix: "+61",   PrefixValue: 61,   Country: "Australia (+61)"                         , NationalLengthMin: 5,  NationalLengthMax: 15, Locked: true),
			new CountryCode(CountryShort: "ID",    Prefix: "+62",   PrefixValue: 62,   Country: "Indonesia (+62)"                         , NationalLengthMin: 5,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "PH",    Prefix: "+63",   PrefixValue: 63,   Country: "Philippines (+63)"                       , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "NZ",    Prefix: "+64",   PrefixValue: 64,   Country: "New Zealand (+64)"                       , NationalLengthMin: 3,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "SG",    Prefix: "+65",   PrefixValue: 65,   Country: "Singapore (+65)"                         , NationalLengthMin: 8,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "TH",    Prefix: "+66",   PrefixValue: 66,   Country: "Thailand (+66)"                          , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "JP",    Prefix: "+81",   PrefixValue: 81,   Country: "Japan (+81)"                             , NationalLengthMin: 5,  NationalLengthMax: 13, Locked: true),
			new CountryCode(CountryShort: "KR",    Prefix: "+82",   PrefixValue: 82,   Country: "Korea, South (+82)"                      , NationalLengthMin: 8,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "VN",    Prefix: "+84",   PrefixValue: 84,   Country: "Vietnam (+84)"                           , NationalLengthMin: 7,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "CN",    Prefix: "+86",   PrefixValue: 86,   Country: "China (+86)"                             , NationalLengthMin: 5,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "TR",    Prefix: "+90",   PrefixValue: 90,   Country: "Turkey (+90)"                            , NationalLengthMin: 10, NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "IN",    Prefix: "+91",   PrefixValue: 91,   Country: "India (+91)"                             , NationalLengthMin: 7,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "PK",    Prefix: "+92",   PrefixValue: 92,   Country: "Pakistan (+92)"                          , NationalLengthMin: 8,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "AF",    Prefix: "+93",   PrefixValue: 93,   Country: "Afghanistan (+93)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "LK",    Prefix: "+94",   PrefixValue: 94,   Country: "Sri Lanka (+94)"                         , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MN",    Prefix: "+95",   PrefixValue: 95,   Country: "Myanmar (+95)"                           , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "IR",    Prefix: "+98",   PrefixValue: 98,   Country: "Iran (+98)"                              , NationalLengthMin: 6,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "SS",    Prefix: "+211",  PrefixValue: 211,  Country: "South Sudan (+211)"                      , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MA/EH", Prefix: "+212",  PrefixValue: 212,  Country: "Morocco (+212) / Western Sahara (+212)"  , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "DZ",    Prefix: "+213",  PrefixValue: 213,  Country: "Algeria (+213)"                          , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "TN",    Prefix: "+216",  PrefixValue: 216,  Country: "Tunisia (+216)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "LY",    Prefix: "+218",  PrefixValue: 218,  Country: "Libya (+218)"                            , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GM",    Prefix: "+220",  PrefixValue: 220,  Country: "Gambia (+220)"                           , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "SN",    Prefix: "+221",  PrefixValue: 221,  Country: "Senegal (+221)"                          , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MR",    Prefix: "+222",  PrefixValue: 222,  Country: "Mauritania (+222)"                       , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "ML",    Prefix: "+223",  PrefixValue: 223,  Country: "Mali (+223)"                             , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "GN",    Prefix: "+224",  PrefixValue: 224,  Country: "Guinea (+224)"                           , NationalLengthMin: 8,  NationalLengthMax: 8, Locked: true),
			new CountryCode(CountryShort: "CI",    Prefix: "+225",  PrefixValue: 225,  Country: "Ivory Coast (+225)"                      , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BF",    Prefix: "+226",  PrefixValue: 226,  Country: "Burkina Faso (+226)"                     , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "NE",    Prefix: "+227",  PrefixValue: 227,  Country: "Niger (+227)"                            , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "TG",    Prefix: "+228",  PrefixValue: 228,  Country: "Togo (+228)"                             , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BJ",    Prefix: "+229",  PrefixValue: 229,  Country: "Benin (+229)"                            , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "MU",    Prefix: "+230",  PrefixValue: 230,  Country: "Mauritius (+230)"                        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "LR",    Prefix: "+231",  PrefixValue: 231,  Country: "Liberia (+231)"                          , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "SL",    Prefix: "+232",  PrefixValue: 232,  Country: "Sierra Leone (+232)"                     , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "GH",    Prefix: "+233",  PrefixValue: 233,  Country: "Ghana (+233)"                            , NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "NG",    Prefix: "+234",  PrefixValue: 234,  Country: "Nigeria (+234)"                          , NationalLengthMin: 7,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "TD",    Prefix: "+235",  PrefixValue: 235,  Country: "Chad (+235)"                             , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CF",    Prefix: "+236",  PrefixValue: 236,  Country: "Central African Republic (+236)"         , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CM",    Prefix: "+237",  PrefixValue: 237,  Country: "Cameroon (+237)"                         , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CV",    Prefix: "+238",  PrefixValue: 238,  Country: "Cape Verde Islands (+238)"               , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "ST",    Prefix: "+239",  PrefixValue: 239,  Country: "São Tomé and Principe (+239)"            , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "GQ",    Prefix: "+240",  PrefixValue: 240,  Country: "Equatorial Guinea (+240)"                , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GA",    Prefix: "+241",  PrefixValue: 241,  Country: "Gabon (+241)"                            , NationalLengthMin: 6,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "CG",    Prefix: "+242",  PrefixValue: 242,  Country: "Congo (+242)"                            , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "CD",    Prefix: "+243",  PrefixValue: 243,  Country: "Congo, Democratic Republic of the (+243)", NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "AO",    Prefix: "+244",  PrefixValue: 244,  Country: "Angola (+244)"                           , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GW",    Prefix: "+245",  PrefixValue: 245,  Country: "Guinea - Bissau (+245)"                  , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "IO",    Prefix: "+246",  PrefixValue: 246,  Country: "British Indian Ocean Territory (+246)"   , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "AC",    Prefix: "+247",  PrefixValue: 247,  Country: "Ascension Island (+247)"                 , NationalLengthMin: 4,  NationalLengthMax: 4,  Locked: true),
			new CountryCode(CountryShort: "SC",    Prefix: "+248",  PrefixValue: 248,  Country: "Seychelles (+248)"                       , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "SD",    Prefix: "+249",  PrefixValue: 249,  Country: "Sudan (+249)"                            , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "RW",    Prefix: "+250",  PrefixValue: 250,  Country: "Rwanda (+250)"                           , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "ET",    Prefix: "+251",  PrefixValue: 251,  Country: "Ethiopia (+251)"                         , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "SO",    Prefix: "+252",  PrefixValue: 252,  Country: "Somalia (+252)"                          , NationalLengthMin: 5,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "DJ",    Prefix: "+253",  PrefixValue: 253,  Country: "Djibouti (+253)"                         , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "KE",    Prefix: "+254",  PrefixValue: 254,  Country: "Kenya (+254)"                            , NationalLengthMin: 6,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "TZ",    Prefix: "+255",  PrefixValue: 255,  Country: "Tanzania (+255)"                         , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "UG",    Prefix: "+256",  PrefixValue: 256,  Country: "Uganda (+256)"                           , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "BI",    Prefix: "+257",  PrefixValue: 257,  Country: "Burundi (+257)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "MZ",    Prefix: "+258",  PrefixValue: 258,  Country: "Mozambique (+258)"                       , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "ZM",    Prefix: "+260",  PrefixValue: 260,  Country: "Zambia (+260)"                           , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MG",    Prefix: "+261",  PrefixValue: 261,  Country: "Madagascar (+261)"                       , NationalLengthMin: 9,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "YT/RE", Prefix: "+262",  PrefixValue: 262,  Country: "Mayotte (+262) / Réunion (+262)"         , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "ZW",    Prefix: "+263",  PrefixValue: 263,  Country: "Zimbabwe (+263)"                         , NationalLengthMin: 5,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "NA",    Prefix: "+264",  PrefixValue: 264,  Country: "Namibia (+264)"                          , NationalLengthMin: 6,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "MW",    Prefix: "+265",  PrefixValue: 265,  Country: "Malawi (+265)"                           , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "LS",    Prefix: "+266",  PrefixValue: 266,  Country: "Lesotho (+266)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BW",    Prefix: "+267",  PrefixValue: 267,  Country: "Botswana (+267)"                         , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "SZ",    Prefix: "+268",  PrefixValue: 268,  Country: "Eswatini (+268)"                         , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "KM",    Prefix: "+269",  PrefixValue: 269,  Country: "Comoros (+269)"                          , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "SH",    Prefix: "+290",  PrefixValue: 290,  Country: "St. Helena (+290)"                       , NationalLengthMin: 4,  NationalLengthMax: 4,  Locked: true),
			new CountryCode(CountryShort: "ER",    Prefix: "+291",  PrefixValue: 291,  Country: "Eritrea (+291)"                          , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "AW",    Prefix: "+297",  PrefixValue: 297,  Country: "Aruba (+297)"                            , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "FO",    Prefix: "+298",  PrefixValue: 298,  Country: "Faroe Islands (+298)"                    , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "GL",    Prefix: "+299",  PrefixValue: 299,  Country: "Greenland (+299)"                        , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "GI",    Prefix: "+350",  PrefixValue: 350,  Country: "Gibraltar (+350)"                        , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "PT",    Prefix: "+351",  PrefixValue: 351,  Country: "Portugal (+351)"                         , NationalLengthMin: 9,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "LU",    Prefix: "+352",  PrefixValue: 352,  Country: "Luxembourg (+352)"                       , NationalLengthMin: 4,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "IE",    Prefix: "+353",  PrefixValue: 353,  Country: "Ireland (+353)"                          , NationalLengthMin: 7,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "IS",    Prefix: "+354",  PrefixValue: 354,  Country: "Iceland (+354)"                          , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "AL",    Prefix: "+355",  PrefixValue: 355,  Country: "Albania (+355)"                          , NationalLengthMin: 3,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MT",    Prefix: "+356",  PrefixValue: 356,  Country: "Malta (+356)"                            , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CY",    Prefix: "+357",  PrefixValue: 357,  Country: "Cyprus (+357)"                           , NationalLengthMin: 8,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "FI",    Prefix: "+358",  PrefixValue: 358,  Country: "Finland (+358)"                          , NationalLengthMin: 5,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "BG",    Prefix: "+359",  PrefixValue: 359,  Country: "Bulgaria (+359)"                         , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "LT",    Prefix: "+370",  PrefixValue: 370,  Country: "Lithuania (+370)"                        , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "LV",    Prefix: "+371",  PrefixValue: 371,  Country: "Latvia (+371)"                           , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "EE",    Prefix: "+372",  PrefixValue: 372,  Country: "Estonia (+372)"                          , NationalLengthMin: 7,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "MD",    Prefix: "+373",  PrefixValue: 373,  Country: "Moldova (+373)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "AM",    Prefix: "+374",  PrefixValue: 374,  Country: "Armenia (+374)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BY",    Prefix: "+375",  PrefixValue: 375,  Country: "Belarus (+375)"                          , NationalLengthMin: 9,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "AD",    Prefix: "+376",  PrefixValue: 376,  Country: "Andorra (+376)"                          , NationalLengthMin: 6,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MC",    Prefix: "+377",  PrefixValue: 377,  Country: "Monaco (+377)"                           , NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "SM",    Prefix: "+378",  PrefixValue: 378,  Country: "San Marino (+378)"                       , NationalLengthMin: 6,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "UA",    Prefix: "+380",  PrefixValue: 380,  Country: "Ukraine (+380)"                          , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "CS",    Prefix: "+381",  PrefixValue: 381,  Country: "Serbia (+381)"                           , NationalLengthMin: 4,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "ME",    Prefix: "+382",  PrefixValue: 382,  Country: "Montenegro (+382)"                       , NationalLengthMin: 4,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "XK",    Prefix: "+383",  PrefixValue: 383,  Country: "Kosovo (+383)"                           , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "HR",    Prefix: "+385",  PrefixValue: 385,  Country: "Croatia (+385)"                          , NationalLengthMin: 8,  NationalLengthMax: 12,  Locked: true),
			new CountryCode(CountryShort: "SI",    Prefix: "+386",  PrefixValue: 386,  Country: "Slovenia (+386)"                         , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BA",    Prefix: "+387",  PrefixValue: 387,  Country: "Bosnia Herzegovina (+387)"               , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "MK",    Prefix: "+389",  PrefixValue: 389,  Country: "Macedonia (+389)"                        , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CZ",    Prefix: "+420",  PrefixValue: 420,  Country: "Czech Republic (+420)"                   , NationalLengthMin: 4,  NationalLengthMax: 12, Locked: true),
			new CountryCode(CountryShort: "SK",    Prefix: "+421",  PrefixValue: 421,  Country: "Slovakia (+421)"                         , NationalLengthMin: 4,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "LI",    Prefix: "+423",  PrefixValue: 423,  Country: "Liechtenstein (+423)"                    , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "FK",    Prefix: "+500",  PrefixValue: 500,  Country: "Falkland Islands (+500)"                 , NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "BZ",    Prefix: "+501",  PrefixValue: 501,  Country: "Belize (+501)"                           , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GT",    Prefix: "+502",  PrefixValue: 502,  Country: "Guatemala (+502)"                        , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "SV",    Prefix: "+503",  PrefixValue: 503,  Country: "El Salvador (+503)"                      , NationalLengthMin: 7,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "HN",    Prefix: "+504",  PrefixValue: 504,  Country: "Honduras (+504)"                         , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "NI",    Prefix: "+505",  PrefixValue: 505,  Country: "Nicaragua (+505)"                        , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "CR",    Prefix: "+506",  PrefixValue: 506,  Country: "Costa Rica (+506)"                       , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "PA",    Prefix: "+507",  PrefixValue: 507,  Country: "Panama (+507)"                           , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "PM",    Prefix: "+508",  PrefixValue: 508,  Country: "St. Pierre and Miquelon (+508)"          , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "HT",    Prefix: "+509",  PrefixValue: 509,  Country: "Haiti (+509)"                            , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "GP",    Prefix: "+590",  PrefixValue: 590,  Country: "Guadeloupe (+590)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "BO",    Prefix: "+591",  PrefixValue: 591,  Country: "Bolivia (+591)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "GY",    Prefix: "+592",  PrefixValue: 592,  Country: "Guyana (+592)"                           , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "EC",    Prefix: "+593",  PrefixValue: 593,  Country: "Ecuador (+593)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "GF",    Prefix: "+594",  PrefixValue: 594,  Country: "French Guiana (+594)"                    , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "PY",    Prefix: "+595",  PrefixValue: 595,  Country: "Paraguay (+595)"                         , NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MQ",    Prefix: "+596",  PrefixValue: 596,  Country: "Martinique (+596)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "SR",    Prefix: "+597",  PrefixValue: 597,  Country: "Suriname (+597)"                         , NationalLengthMin: 6,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "UY",    Prefix: "+598",  PrefixValue: 598,  Country: "Uruguay (+598)"                          , NationalLengthMin: 4,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "BQ",    Prefix: "+599",  PrefixValue: 599,  Country: "Bonaire, Saba and Sint Eustatius (+599)" , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "TL",    Prefix: "+670",  PrefixValue: 670,  Country: "East Timor (+670)"                       , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "NF",    Prefix: "+672",  PrefixValue: 672,  Country: "Norfolk Islands (+672)"                  , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "BN",    Prefix: "+673",  PrefixValue: 673,  Country: "Brunei (+673)"                           , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "NR",    Prefix: "+674",  PrefixValue: 674,  Country: "Nauru (+674)"                            , NationalLengthMin: 4,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "PG",    Prefix: "+675",  PrefixValue: 675,  Country: "Papua New Guinea (+675)"                 , NationalLengthMin: 4,  NationalLengthMax: 11, Locked: true),
			new CountryCode(CountryShort: "TO",    Prefix: "+676",  PrefixValue: 676,  Country: "Tonga (+676)"                            , NationalLengthMin: 5,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "SB",    Prefix: "+677",  PrefixValue: 677,  Country: "Solomon Islands (+677)"                  , NationalLengthMin: 5,  NationalLengthMax: 5,  Locked: true),
			new CountryCode(CountryShort: "VU",    Prefix: "+678",  PrefixValue: 678,  Country: "Vanuatu (+678)"                          , NationalLengthMin: 5,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "FJ",    Prefix: "+679",  PrefixValue: 679,  Country: "Fiji (+679)"                             , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "PW",    Prefix: "+680",  PrefixValue: 680,  Country: "Palau (+680)"                            , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "WF",    Prefix: "+681",  PrefixValue: 681,  Country: "Wallis and Futuna (+681)"                , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "CK",    Prefix: "+682",  PrefixValue: 682,  Country: "Cook Islands (+682)"                     , NationalLengthMin: 5,  NationalLengthMax: 5,  Locked: true),
			new CountryCode(CountryShort: "NU",    Prefix: "+683",  PrefixValue: 683,  Country: "Niue (+683)"                             , NationalLengthMin: 4,  NationalLengthMax: 4,  Locked: true),
			new CountryCode(CountryShort: "WS",    Prefix: "+685",  PrefixValue: 685,  Country: "Samoa (+685)"                            , NationalLengthMin: 3,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "KI",    Prefix: "+686",  PrefixValue: 686,  Country: "Kiribati (+686)"                         , NationalLengthMin: 5,  NationalLengthMax: 5,  Locked: true),
			new CountryCode(CountryShort: "NC",    Prefix: "+687",  PrefixValue: 687,  Country: "New Caledonia (+687)"                    , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "TV",    Prefix: "+688",  PrefixValue: 688,  Country: "Tuvalu (+688)"                           , NationalLengthMin: 5,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "PF",    Prefix: "+689",  PrefixValue: 689,  Country: "French Polynesia (+689)"                 , NationalLengthMin: 6,  NationalLengthMax: 6,  Locked: true),
			new CountryCode(CountryShort: "TK",    Prefix: "+690",  PrefixValue: 690,  Country: "Tokelau (+690)"                          , NationalLengthMin: 4,  NationalLengthMax: 4,  Locked: true),
			new CountryCode(CountryShort: "FM",    Prefix: "+691",  PrefixValue: 691,  Country: "Micronesia (+691)"                       , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "MH",    Prefix: "+692",  PrefixValue: 692,  Country: "Marshall Islands (+692)"                 , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "KP",    Prefix: "+850",  PrefixValue: 850,  Country: "Korea, North (+850)"                     , NationalLengthMin: 6,  NationalLengthMax: 17, Locked: true),
			new CountryCode(CountryShort: "HK",    Prefix: "+852",  PrefixValue: 852,  Country: "Hong Kong (+852)"                        , NationalLengthMin: 4,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MO",    Prefix: "+853",  PrefixValue: 853,  Country: "Macao (+853)"                            , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "KH",    Prefix: "+855",  PrefixValue: 855,  Country: "Cambodia (+855)"                         , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "LA",    Prefix: "+856",  PrefixValue: 856,  Country: "Laos (+856)"                             , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "BD",    Prefix: "+880",  PrefixValue: 880,  Country: "Bangladesh (+880)"                       , NationalLengthMin: 6,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "TW",    Prefix: "+886",  PrefixValue: 886,  Country: "Taiwan (+886)"                           , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "MV",    Prefix: "+960",  PrefixValue: 960,  Country: "Maldives (+960)"                         , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "LB",    Prefix: "+961",  PrefixValue: 961,  Country: "Lebanon (+961)"                          , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "JO",    Prefix: "+962",  PrefixValue: 962,  Country: "Jordan (+962)"                           , NationalLengthMin: 5,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "SI",    Prefix: "+963",  PrefixValue: 963,  Country: "Syria (+963)"                            , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "IQ",    Prefix: "+964",  PrefixValue: 964,  Country: "Iraq (+964)"                             , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "KW",    Prefix: "+965",  PrefixValue: 965,  Country: "Kuwait (+965)"                           , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "SA",    Prefix: "+966",  PrefixValue: 966,  Country: "Saudi Arabia (+966)"                     , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "YE",    Prefix: "+967",  PrefixValue: 967,  Country: "Yemen (+967)"                            , NationalLengthMin: 6,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "OM",    Prefix: "+968",  PrefixValue: 968,  Country: "Oman (+968)"                             , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "PS",    Prefix: "+970",  PrefixValue: 970,  Country: "Palestine (+970)"                        , NationalLengthMin: 7,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "AE",    Prefix: "+971",  PrefixValue: 971,  Country: "United Arab Emirates (+971)"             , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "IL",    Prefix: "+972",  PrefixValue: 972,  Country: "Israel (+972)"                           , NationalLengthMin: 8,  NationalLengthMax: 10, Locked: true),
			new CountryCode(CountryShort: "BH",    Prefix: "+973",  PrefixValue: 973,  Country: "Bahrain (+973)"                          , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "QA",    Prefix: "+974",  PrefixValue: 974,  Country: "Qatar (+974)"                            , NationalLengthMin: 3,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "BT",    Prefix: "+975",  PrefixValue: 975,  Country: "Bhutan (+975)"                           , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "MN",    Prefix: "+976",  PrefixValue: 976,  Country: "Mongolia (+976)"                         , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "NP",    Prefix: "+977",  PrefixValue: 977,  Country: "Nepal (+977)"                            , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "TJ",    Prefix: "+992",  PrefixValue: 992,  Country: "Tajikstan (+992)"                        , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "TM",    Prefix: "+993",  PrefixValue: 993,  Country: "Turkmenistan (+993)"                     , NationalLengthMin: 8,  NationalLengthMax: 8,  Locked: true),
			new CountryCode(CountryShort: "AZ",    Prefix: "+994",  PrefixValue: 994,  Country: "Azerbaijan (+994)"                       , NationalLengthMin: 8,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "GE",    Prefix: "+995",  PrefixValue: 995,  Country: "Georgia (+995)"                          , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "KG",    Prefix: "+996",  PrefixValue: 996,  Country: "Kyrgyzstan (+996)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "UZ",    Prefix: "+998",  PrefixValue: 998,  Country: "Uzbekistan (+998)"                       , NationalLengthMin: 9,  NationalLengthMax: 9,  Locked: true),
			new CountryCode(CountryShort: "BS",    Prefix: "+1242", PrefixValue: 1242, Country: "Bahamas (+1242)"                         , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "BB",    Prefix: "+1246", PrefixValue: 1246, Country: "Barbados (+1246)"                        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "AI",    Prefix: "+1264", PrefixValue: 1264, Country: "Anguilla (+1264)"                        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "AG",    Prefix: "+1268", PrefixValue: 1268, Country: "Antigua and Barbuda (+1268)"             , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "VG",    Prefix: "+1284", PrefixValue: 1284, Country: "Virgin Islands - British (+1284)"        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "VI",    Prefix: "+1340", PrefixValue: 1340, Country: "Virgin Islands - US (+1340)"             , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "KY",    Prefix: "+1345", PrefixValue: 1345, Country: "Cayman Islands (+1345)"                  , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "BM",    Prefix: "+1441", PrefixValue: 1441, Country: "Bermuda (+1441)"                         , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "GD",    Prefix: "+1473", PrefixValue: 1473, Country: "Grenada (+1473)"                         , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "TC",    Prefix: "+1649", PrefixValue: 1649, Country: "Turks and Caicos Islands (+1649)"        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "MS",    Prefix: "+1664", PrefixValue: 1664, Country: "Montserrat (+1664)"                      , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "NP",    Prefix: "+1670", PrefixValue: 1670, Country: "Northern Mariana Islands (+1670)"        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "GU",    Prefix: "+1671", PrefixValue: 1671, Country: "Guam (+1671)"                            , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "SC",    Prefix: "+1758", PrefixValue: 1758, Country: "St. Lucia (+1758)"                       , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "DM",    Prefix: "+1767", PrefixValue: 1767, Country: "Dominica (+1767)"                        , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "PR",    Prefix: "+1787", PrefixValue: 1787, Country: "Puerto Rico (+1787)"                     , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "DO",    Prefix: "+1809", PrefixValue: 1809, Country: "Dominican Republic (+1809)"              , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "TT",    Prefix: "+1868", PrefixValue: 1868, Country: "Trinidad and Tobago (+1868)"             , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "KN",    Prefix: "+1869", PrefixValue: 1869, Country: "St. Kitts and Nevis (+1869)"             , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "JM",    Prefix: "+1876", PrefixValue: 1876, Country: "Jamaica (+1876)"                         , NationalLengthMin: 7,  NationalLengthMax: 7,  Locked: true),
			new CountryCode(CountryShort: "CW",    Prefix: "+5999", PrefixValue: 5999, Country: "Curaçao (+5999)"                         , NationalLengthMin: 7,  NationalLengthMax: 8,  Locked: true),
		};

		private static readonly PhoneRange[] NumberList = new PhoneRange[]
		{
			new PhoneRange() { Start = 0, End = 0, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in Australia" },
			new PhoneRange() { Start = 100, End = 100, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in India, Greece and Israel" },
			new PhoneRange() { Start = 106, End = 106, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in Australia for textphone/TTY" },
			new PhoneRange() { Start = 108, End = 108, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in India (22 states)" },
			new PhoneRange() { Start = 110, End = 110, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number mainly in China, Japan, Taiwan" },
			new PhoneRange() { Start = 111, End = 111, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in New Zealand" },
			new PhoneRange() { Start = 114, End = 114, Allowed = false, Type = NumberType.Emergency, Description = "Politiið / Police" },
			new PhoneRange() { Start = 118, End = 118, Allowed = false, Type = NumberType.Emergency, Description = "Nummarupplýsing / Number Info" },
			new PhoneRange() { Start = 112, End = 112, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number across the European Union and on GSM mobile networks across the world" },
			new PhoneRange() { Start = 119, End = 119, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in Jamaica and parts of Asia" },
			new PhoneRange() { Start = 122, End = 122, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number for specific services in several countries" },
			new PhoneRange() { Start = 911, End = 911, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in North America and the Philippines" },
			new PhoneRange() { Start = 999, End = 999, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in many countries" },
			new PhoneRange() { Start = 1000, End = 1000, Allowed = false, Type = NumberType.Emergency, Description = "Emergency number in many countries" },
			new PhoneRange() { Start = 200000, End = 209999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 210000, End = 219999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 220000, End = 229999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 230000, End = 239999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 240000, End = 249999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 250000, End = 259999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 260000, End = 269999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 270000, End = 279999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 280000, End = 285999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 286000, End = 288999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 289000, End = 289999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 290000, End = 299999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 300000, End = 309999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 310000, End = 319999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 320000, End = 329999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 330000, End = 339999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 340000, End = 349999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 350000, End = 359999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 360000, End = 369999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 370000, End = 379999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 380000, End = 389999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 390000, End = 399999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 400000, End = 409999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 410000, End = 419999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 420000, End = 429999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 430000, End = 439999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 440000, End = 449999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 450000, End = 459999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 460000, End = 469999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 470000, End = 479999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 480000, End = 489999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 490000, End = 499999, Allowed = false, Type = NumberType.Fixed, Description = "Faroese Telecom fixed network" },
			new PhoneRange() { Start = 500000, End = 509999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 510000, End = 519999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 520000, End = 529999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 530000, End = 539999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 540000, End = 549999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 550000, End = 559999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 560000, End = 569999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 570000, End = 579999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 580000, End = 589999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 590000, End = 599999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 600000, End = 609999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 610000, End = 619999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 620000, End = 629999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 630000, End = 639999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 640000, End = 649999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 650000, End = 659999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 660000, End = 669999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 670000, End = 679999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 680000, End = 689999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 690000, End = 699999, Allowed = false, Type = NumberType.IP, Description = "Faroese Telecom IP-Phones" },
			new PhoneRange() { Start = 700000, End = 700999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 701000, End = 701999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 702000, End = 702999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 703000, End = 703000, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 704000, End = 704999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 705000, End = 705999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 706000, End = 706999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 707000, End = 707999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 708000, End = 708999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 709000, End = 709999, Allowed = false, Type = NumberType.SharedCost, Description = "Fixed network shared cost numbers" },
			new PhoneRange() { Start = 710000, End = 719999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 720000, End = 729999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 730000, End = 739999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 740000, End = 749999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 750000, End = 759999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 760000, End = 769999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 770000, End = 779999, Allowed = true, Type = NumberType.GSM, Description = "Hey GSM" },
			new PhoneRange() { Start = 780000, End = 789999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 790000, End = 794999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 795000, End = 799999, Allowed = true, Type = NumberType.GSM, Description = "Faroese Telecom GSM" },
			new PhoneRange() { Start = 801000, End = 801999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 802000, End = 802999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 803000, End = 803999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 804000, End = 804999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 805000, End = 805999, Allowed = false, Type = NumberType.Free, Description = "Hey freephone numbers" },
			new PhoneRange() { Start = 806000, End = 806999, Allowed = false, Type = NumberType.Free, Description = "Hey freephone numbers" },
			new PhoneRange() { Start = 807000, End = 807999, Allowed = false, Type = NumberType.Free, Description = "Hey freephone numbers" },
			new PhoneRange() { Start = 808000, End = 808999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 809000, End = 809999, Allowed = false, Type = NumberType.Free, Description = "Faroese Telecom freephone numbers" },
			new PhoneRange() { Start = 810000, End = 819999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 820000, End = 829999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 830000, End = 839999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 840000, End = 849999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 850000, End = 859999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 860000, End = 869999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 870000, End = 879999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 880000, End = 889999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 890000, End = 899999, Allowed = false, Type = NumberType.Fixed, Description = "Hey fixed network" },
			new PhoneRange() { Start = 900000, End = 900999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901000, End = 901099, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901100, End = 901199, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901200, End = 901299, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901300, End = 901399, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901400, End = 901499, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901500, End = 901599, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901600, End = 901699, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901700, End = 901799, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901800, End = 901899, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 901900, End = 901999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902000, End = 902099, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902100, End = 902199, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902200, End = 902299, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902300, End = 902399, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902400, End = 902499, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902500, End = 902599, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902600, End = 902699, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902700, End = 902799, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902800, End = 902999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 902900, End = 902999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903000, End = 903099, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903100, End = 903199, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903200, End = 903299, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903300, End = 903399, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903400, End = 903499, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903500, End = 903599, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903600, End = 903699, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903700, End = 903799, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903800, End = 903899, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 903900, End = 903999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904000, End = 904099, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904100, End = 904199, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904200, End = 904299, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904300, End = 904399, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904400, End = 904499, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904500, End = 904599, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904600, End = 904699, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904700, End = 904799, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904800, End = 904899, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 904900, End = 904999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905000, End = 905099, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905100, End = 905199, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905200, End = 905300, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905300, End = 905399, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905400, End = 905499, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905500, End = 905500, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905600, End = 905699, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905700, End = 905799, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905800, End = 905899, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 905900, End = 905999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 906000, End = 908999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 909000, End = 909999, Allowed = false, Type = NumberType.Service, Description = "Service numbers" },
			new PhoneRange() { Start = 910000, End = 919999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 920000, End = 929999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 930000, End = 939999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 940000, End = 949999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 950000, End = 959999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 960000, End = 969999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 970000, End = 979999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 980000, End = 989999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
			new PhoneRange() { Start = 990000, End = 999999, Allowed = true, Type = NumberType._3G, Description = "Reserved to 3G" },
		};

		public static CountryCode GetCountry(string Code)
		{
			if (string.IsNullOrWhiteSpace(Code))
				return null;
			if (!HasPrefix(Code))
				return null;

			Code = SanitizeNumber(Code);
			if (Code.Length > 1)
			{
				if (Code.StartsWith("00"))
					Code = Code.Substring(2, Code.Length - 2);
				else if (Code.StartsWith("+"))
					Code = Code.Substring(1, Code.Length - 1);

				if (Code.Length > 1 && Code.All(x => char.IsDigit(x)))
				{
					CountryCode FoundCountry = null;

					char firstChar = Code.FirstOrDefault();
					if (firstChar == '7')
					{
						// Only Russia starts with 7.
						FoundCountry = CountryPrefixList.FirstOrDefault(x =>
							x.PrefixValue == 7 &&
							x.LengthValid(Code.Length - (x.Prefix.Length - 1))
						);
					}
					else if (firstChar == '1')
					{
						// Check if Non-US country.
						string possibleCountry = Code.Length > 4 ? Code.Substring(0, 4) : null;
						if (possibleCountry != null)
						{
							FoundCountry = CountryPrefixList.FirstOrDefault(x =>
								x.Prefix.Length > 4 &&
								x.Prefix.Skip(1).SequenceEqual(possibleCountry) &&
								x.LengthValid(Code.Length - (x.Prefix.Length - 1))
							);
						}

						// If non are found then it's US.
						if (FoundCountry == null)
						{
							FoundCountry = CountryPrefixList.FirstOrDefault(x =>
								x.PrefixValue == 1 &&
								x.LengthValid(Code.Length - (x.Prefix.Length - 1))
							);
						}
					}
					else
					{
						string possibleCountry;

						// Check if its country with two digit prefix.
						possibleCountry = Code.Length > 2 ? Code.Substring(0, 2) : null;
						if (possibleCountry != null)
						{
							FoundCountry = CountryPrefixList.FirstOrDefault(x =>
								x.Prefix.Length > 2 &&
								x.Prefix.Skip(1).SequenceEqual(possibleCountry) &&
								x.LengthValid(Code.Length - (x.Prefix.Length - 1))
							);
						}

						// Else check if its country with three digit prefix.
						if (FoundCountry == null)
						{
							possibleCountry = Code.Length > 3 ? Code.Substring(0, 3) : null;

							FoundCountry = CountryPrefixList.FirstOrDefault(x =>
								x.Prefix.Length > 2 &&
								x.Prefix.Skip(1).SequenceEqual(possibleCountry) &&
								x.LengthValid(Code.Length - (x.Prefix.Length - 1))
							);
						}
					}

					return FoundCountry;
				}
			}

			return null;
		}

		public static string SanitizeNumber(string number)
		{
			if (number == null)
				return null;
			if (number.Length == 0)
				return number;

			int size = number.Length;
			StringBuilder cleaned = new StringBuilder(size);
			bool foundFirst = false;
			for (int i = 0; i < size; i++)
			{
				if (!char.IsWhiteSpace(number[i]))
				{
					bool IsDigit = char.IsDigit(number[i]);
					if (IsDigit || (!foundFirst ? number[i] == '+' : false))
					{
						foundFirst = IsDigit;
						cleaned.Append(number[i]);
					}
				}
			}

			return cleaned.ToString();
		}

		public static bool IsGSMNumber(string number)
		{
			if (int.TryParse(number.Replace("+", string.Empty).Trim(), out int result))
				return IsGSMNumber(result);

			return false;
		}

		public static bool IsGSMNumber(int number)
		{
			int minNum = 0;
			int maxNum = NumberList.Length - 1;

			while (minNum <= maxNum)
			{
				int mid = (minNum + maxNum) / 2;
				if (number >= NumberList[mid].Start && number <= NumberList[mid].End)
					return NumberList[mid].Type == NumberType.GSM || NumberList[mid].Type == NumberType._3G;
				else if (NumberList[mid].Start < number)
					minNum++;
				else
					maxNum--;
			}

			return false;
		}

		public static bool IsFaroeseGSMNumber(string number)
		{
			Tuple<int?, CountryCode> PhoneNumber = CleanNumber(number);
			if (PhoneNumber.Item1 != null && PhoneNumber.Item2 != null && string.Equals(PhoneNumber.Item2.CountryShort, "FO", StringComparison.OrdinalIgnoreCase))
			{
				PhoneRange FoundNumber = FindPhoneNumberRange(PhoneNumber.Item1.Value);
				if (FoundNumber != null)
					return FoundNumber.Type == NumberType.GSM || FoundNumber.Type == NumberType._3G;
			}

			return false;
		}

		public static bool IsFaroeseNumber(string number)
		{
			Tuple<int?, CountryCode> PhoneNumber = CleanNumber(number);
			if (PhoneNumber.Item1 != null && PhoneNumber.Item2 != null && string.Equals(PhoneNumber.Item2.CountryShort, "FO", StringComparison.OrdinalIgnoreCase))
			{
				PhoneRange FoundNumber = FindPhoneNumberRange(PhoneNumber.Item1.Value);
				return FoundNumber != null;
			}

			return false;
		}

		private static PhoneRange FindPhoneNumberRange(int number)
		{
			return NumberList.FirstOrDefault(x => number >= x.Start && number <= x.End);
		}

		private static Tuple<int?, CountryCode> CleanNumber(string number)
		{
			if (number != null)
			{
				CountryCode FoundCountry = null;
				number = number.Replace(" ", string.Empty);
				if (number.StartsWith("00") || number.StartsWith("+"))
				{
					if (number.StartsWith("00"))
						number = $"+{ number.Substring(2, number.Length - 2) }";

					FoundCountry = GetCountry(number);
					if (FoundCountry != null && number.Length >= FoundCountry.Prefix.Length)
						number = number.Substring(FoundCountry.Prefix.Length, number.Length - FoundCountry.Prefix.Length);
				}
				else
				{
					if (number.Length == 6)
						FoundCountry = CountryPrefixList.FirstOrDefault(x => string.Equals(x.CountryShort, "FO", StringComparison.OrdinalIgnoreCase));
				}

				if (!string.IsNullOrWhiteSpace(number) && number.All(x => char.IsDigit(x)))
				{
					if (int.TryParse(number, out int parseResult))
						return Tuple.Create<int?, CountryCode>(parseResult, FoundCountry);
				}
			}

			return Tuple.Create<int?, CountryCode>(null, null);
		}

		public static bool HasPrefix(string number)
		{
			number = number.Trim();
			return number.StartsWith("+") || number.StartsWith("00");
		}
	}
}
