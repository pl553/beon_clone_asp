var _____WB$wombat$assign$function_____ = function(name) {return (self._wb_wombat && self._wb_wombat.local_init && self._wb_wombat.local_init(name)) || self[name]; };
if (!self.__WB_pmw) { self.__WB_pmw = function(obj) { this.__WB_source = obj; return this; } }
{
  let window = _____WB$wombat$assign$function_____("window");
  let self = _____WB$wombat$assign$function_____("self");
  let document = _____WB$wombat$assign$function_____("document");
  let location = _____WB$wombat$assign$function_____("location");
  let top = _____WB$wombat$assign$function_____("top");
  let parent = _____WB$wombat$assign$function_____("parent");
  let frames = _____WB$wombat$assign$function_____("frames");
  let opener = _____WB$wombat$assign$function_____("opener");

var j=0;
//-----------------------------------------------------------------------------
function IconImage(p_img_src, p_element) {
	this.img = new Image();
	this.img_element = null;
	this.counter = 0;

	var self = this;
	this.check_complete = function() {
		if(!self.img.complete) {
			if(self.counter++ < 300) {
				setTimeout(self.check_complete, 1000);
			}else {
				self.img_element.src	= 'http://web.archive.org/web/20070705045621/http://beon.ru/i/emp.gif';
			}
			return;
		}

		var elem = self.img_element;
		if(self.img.width >= 16 && self.img.height >= 16) {
			elem.src	= self.img.src;
			elem.hspace	= 2;
			elem.width	= 16;
			elem.height	= 16;
			return;
		}

		elem.src	= 'http://web.archive.org/web/20070705045621/http://beon.ru/i/emp.gif';
		return;
	}

	this.src(p_img_src);
	this.element(p_element);
}
//-----------------------------------------------------------------------------
IconImage.prototype.src = function(p_src) {
	if(p_src != undefined) {
		this.img.src = p_src;
	}
	return this.img.src;
}
//-----------------------------------------------------------------------------
IconImage.prototype.element = function(p_element) {
	this.img_element = p_element;
}
//-----------------------------------------------------------------------------
function favicons_processor() {
	var page_links		 = document.links;
	var linkList		 = new Array();
	var faviconLinkList	 = new Array();
	var faviconLinkList1 = new Array();
	var page_links_length = page_links.length;
	for (var i=0; i<page_links_length; i++){
		var page_link=page_links[i];
		var linkclass = page_link.className;
		if(linkclass == 'favicon_processed') {
			continue;
		}
		if (page_link.href.match(/\.mp3$/i)) {
			linkList.push(page_link);
		}else if (linkclass == 'needfavicon') {
			faviconLinkList.push(page_link);
		}
		else if (linkclass == 'needfavicon1') {
			faviconLinkList1.push(page_link);
		}
	}
	var linkList_length = linkList.length;
	for (var i=0; i<linkList_length; i++){
		var link = linkList[i];
		var span = document.createElement("span");
		span.innerHTML = "&nbsp;<a href=http://beon.ru/p/play_mp3.cgi?url=" + escape(link.href) + " title=\"Слушать\" target=_blank onclick=\"return ShowResponse(this);\" class=\"favicon_processed\"><img class=rad border=0 width=17 height=17 src=http://beon.ru/i/play.png></a>";
		link.parentNode.insertBefore(span, link.nextSibling);
		link.className = 'favicon_processed';
	}
	var faviconLinkList_length = faviconLinkList.length;
	for (var i=0; i<faviconLinkList_length; i++){
		var link		= faviconLinkList[i];
		var image_url	= 'http://'+link.hostname+'/favicon.ico';
		link.innerHTML	= "<img class=flag id=fav"+j+" name=fav"+j+" border=0 width=1 height=1 src="+image_url+">" + link.innerHTML;

		var new_img	= new IconImage(image_url, document.getElementById('fav'+j));
		new_img.check_complete();
	
		link.className = 'favicon_processed';
		j++;
	}
	var faviconLinkList1_length = faviconLinkList1.length;
	for (var i=0; i<faviconLinkList1_length; i++){
		var link = faviconLinkList1[i];
		link.innerHTML = "<img class=flag id=fav"+j+" name=fav"+j+" border=0 width=16 height=16 hspace=2 src=http://beon.ru/i/emp.gif>"+link.innerHTML;
		var new_img	= new IconImage("http://"+link.hostname+"/favicon.ico", document.getElementById('fav'+j));
		new_img.check_complete();

		link.className = 'favicon_processed';
		j++;
	}
}
//-----------------------------------------------------------------------------
favicons_processor();




}
/*
     FILE ARCHIVED ON 04:56:21 Jul 05, 2007 AND RETRIEVED FROM THE
     INTERNET ARCHIVE ON 08:31:03 Aug 23, 2022.
     JAVASCRIPT APPENDED BY WAYBACK MACHINE, COPYRIGHT INTERNET ARCHIVE.

     ALL OTHER CONTENT MAY ALSO BE PROTECTED BY COPYRIGHT (17 U.S.C.
     SECTION 108(a)(3)).
*/
/*
playback timings (ms):
  captures_list: 251.097
  exclusion.robots: 0.322
  exclusion.robots.policy: 0.309
  RedisCDXSource: 1.11
  esindex: 0.012
  LoadShardBlock: 230.832 (3)
  PetaboxLoader3.datanode: 260.764 (4)
  CDXLines.iter: 15.404 (3)
  load_resource: 82.569
  PetaboxLoader3.resolve: 47.841
*/