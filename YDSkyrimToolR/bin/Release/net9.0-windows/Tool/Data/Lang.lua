if not Lang then
	Lang = {};
end
Lang.Loaded = false;
Lang.LocaleID = 0; -- https://msdn.microsoft.com/ru-ru/library/windows/desktop/dd318693(v=vs.85).aspx
Lang.Storage = nil;

local io = io;
local string = string;
local table = table;
local debug = debug;
local type = type;

LANG_NOLANGFILES = 1000;
LANG_CANTLOADFILE = 1001;

local function Traceback()
	return debug.traceback();
end

function Lang.IsLoaded()
	return Lang.Loaded;
end
function Lang.SetLoaded(bLoaded)
	Lang.Loaded = bLoaded or false;
end
function Lang.SetLocaleID(nValue)
	Lang.LocaleID = nValue;
end
function Lang.GetName()
	return SystemSettings.GetLangName(Lang.LocaleID).LangName;
end
function Lang.SetStorage(vData)
	Lang.Storage = vData;
end
function Lang.GetStorage()
	 return Lang.Storage;
end

local function magiclines(str)
	local pos = 1;
	return function()
		if not pos then return nil end
		local  p1, p2 = string.find(str, "\031", pos)
		local line
		if p1 then
			line = str:sub(pos, p1-1);
			pos = p2+1;
		else
			line = str:sub(pos )
			pos = nil;
		end
		return line
	end
end
local function ProcessNewLine(sString)
	local t = {};
	for s in magiclines(sString, "[^\031]*") do
		if s ~= nil then
			t[#t+1] = s;
		end
	end
	--
	return table.concat(t, "\r\n", 1, #t);
end
local function Error(vData)
	local sData = string.format("[Language API] %s", tostring(vData))
	-- error(sData);
	-- App.MessageBox(sData);
end
--
function Lang.PushError(nErrorID)
	if nErrorID == LANG_NOLANGFILES then
		Error("No localization files found");
	elseif nErrorID == LANG_CANTLOADFILE then
		Error("Can't load translation file");
	end
end
--
function Lang.Load(sPath)
	if Lang.IsLoaded() ~= true then
		local tLang = XML();
		local bSuccess = tLang:LoadFile(sPath);
		--
		if bSuccess then
			Lang.SetStorage(tLang);
			Lang.Loaded = true;
			
			local sLocaleID = tLang:GetAttribute("Lang", "LocaleID");
			if (sLocaleID ~= nil) then
				Lang.SetLocaleID(tonumber(sLocaleID:Mid(3, -1), 16));
			end
			--
			if tLang:Count("Lang/Strings") == 0 then
				Error(string.format("Translation strings not found"));
			end
		else
			Error(string.format("Can't load translation file:\n\n%s", tostring(sPath)));
		end
	else
		Error(string.format("Language file \"%s\" already loaded", tostring(sPath)));
	end
end
function Lang.Unload()
	if (Lang.IsLoaded() == true) then
		local tLang = Lang.GetStorage();
		tLang:Cast(LObject):Destroy();
	end
	Lang.Loaded = false;
end
--
local function GetStringByID(sID)
	local tLang = Lang.GetStorage();
	for x, y in ipairs(tLang:GetElementNames("Lang/Strings", true, true)) do
		if tLang:GetAttribute(y, "ID") == sID then
			return tLang:GetValue(y);
		end
	end
end
function Lang.GetStringE(sID, bRaw, ...)
	if type(sID) == "string" and sID ~= "" then
		local tLang = Lang.GetStorage();
		if tLang ~= nil then
			local sString = GetStringByID(sID);
			if sString ~= nil then
				if bRaw == true then
					return sString;
				else
					local bStatus, sFormatted = pcall(string.format, sString, ...);
					if bStatus == true then					
						return sFormatted;
					else
						return sString;
					end
				end
			else
				Error(string.format("Can't load translated string for: %s\r\n%s\r\n", tostring(sID), Traceback()));
			end
		else
			Error(string.format("Can't load translation structure: %s\r\n%s\r\n", tostring(tLang), Traceback()));
		end
	else
		Error(string.format("Incorrect string ID: %s\r\n%s\r\n", tostring(sID), Traceback()));
	end
end
function Lang.GetString(sID, ...)
	local function Get(...)
		return Lang.GetStringE(sID, ...);
	end
	--
	local bStatus, sString = xpcall(Get, Error);
	if bStatus == true then
		if sString == nil then
			Lang.Status = false;
			Error("Error #1. ID: "..tostring(sID));
			return "#E1:"..sID;
		else
			Lang.Status = true;
			return sString;
		end
	else
		Lang.Status = false;
		Error("Error #2. ID: "..tostring(sID)..", String: "..tostring(sString));
		return "#E2:"..sID;
	end
end
function Lang.GetStringRaw(sID)
	local function Get()
		return Lang.GetStringE(sID, true);
	end
	--
	local bStatus, sString = xpcall(Get, Error);
	if bStatus == true then
		if sString == nil then
			Lang.Status = false;
			Error("Error #1. ID: "..tostring(sID));
			return "Error #1", Lang.Status;
		else
			Lang.Status = true;
			return sString, Lang.Status;
		end
	else
		Lang.Status = false;
		Error("Error #2. ID: "..tostring(sID)..", String: "..tostring(sString));
		return "Error #2", Lang.Status;
	end
end
function Lang.GetStringAlt(sID, sAltString, ...)
	local sString = Lang.GetString(sID, false, ...);
	if Lang.Status == true then
		return sString;
	else
		return sAltString or sString;
	end
end

function L(...)
	return Lang.GetString(...);
end
