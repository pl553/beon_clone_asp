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

function getError(msg, url,lno) {
	var img = new Image;

	img.src = 'http://web.archive.org/web/20070705045904/http://beon.ru/js_errors?m='+escape(msg)  + '&u='+escape(url) + '&l=' + escape(lno) + '&r='+escape(document.referrer)+'&'+Math.random();
	return true;
}
if (window.opera && window.addEventListener) {
	window.addEventListener("error", getError, false);
}
else {
	window.onerror = getError;
}

document.cookie = "tz="+escape((new Date()).getTimezoneOffset())+"; path=/; domain=.beon.ru; expires=Wed, 10 Dec 2014 00:35:16 GMT";
document.cookie = "tz="+escape((new Date()).getTimezoneOffset())+"; path=/; expires=Wed, 10 Dec 2014 00:35:16 GMT";
function CheckAvatar(j,context,avatar){
	var f=document.forms[context];
	if(f){
		var m=f.avatar.value = avatar;
		if (document.getElementById) {
			i=0;
			while(document.getElementById('a'+i)) {
				document.getElementById('a'+i).style.borderColor = '#ffffff';
				i++;
			}
			j.style.borderColor = '#808080';
		}
	}
}
function deleteCookie(n){
	document.cookie = n+"=; path=/; domain=.beon.ru; expires=Thu, 01 Jan 1970 00:00:01 GMT";
	document.cookie = n+"=; path=/; domain=."+document.location.hostname+"; expires=Thu, 01 Jan 1970 00:00:01 GMT";
}
function getCookie(n){
	var c = document.cookie;
	if (c.length < 1) return false;
	var b = c.indexOf(n + '=');
	if (b == -1) return false;
	b += (n.length + 1);
	var e = c.indexOf(';', b);
	return unescape((e == -1) ? c.substring(b) : c.substring(b, e));
}
function feelLogin(){
	var f = document.forms['forma'];
	if (document.getElementById && f) {
		var l = getCookie('login');
		if (l && l.length > 4) {
			if (f.login.value != l) {
				f.login.value = l;
				f.pass.value = '';
			}
		}
		var rm = getCookie('remember_me');
		if (rm) {
			if (rm == 'Y') {
				f.remember_me.checked = true;
			}
			else{
				f.remember_me.checked = false;
			}
		}
		document.getElementById('form_div').style.display='block';
		if(f.login.value == null) {
			return false;
		}
		f.login.value.length>4?f.pass.focus():f.login.focus();
		return false;
	}
	return true;
}

function ShowResponse(a)
{
	var str="toolbar=0,scrollbars=0,location=0,statusbar=1,menubar=0,resizable=1,width=300,height=185";
	if(window.screen){
		var xc=screen.availWidth/2-140;
		var yc=screen.availHeight/2-115;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=140";
	}
	if (window.open(a.href,'',str)) {return false;}
	else{return true;}
}
var image_context;
var image_timeout_id;
function checkWindow(){
	var image_code=getCookie('imagecookie');
	if (!image_code) {
		clearTimeout(image_timeout_id);
		image_timeout_id=setTimeout("checkWindow();",99);
		return;
	}
	deleteCookie('imagecookie');
	clearTimeout(image_timeout_id);

	var f = document.forms[image_context];
	if (f) {
		if (image_code){
			m=f.message.value += image_code;
		}
		return;
	}
	var image_context_element = document.getElementById(image_context);
	if(image_context_element) {
		image_context_element.value += image_code;
	}
}
function addImage(context){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=780,height=466";
	if(window.screen){
		var xc=screen.availWidth/2-390;
		var yc=screen.availHeight/2-233;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('imagecookie');
	window.open("http://"+document.location.hostname+"/p/add_image.cgi","",str);
	image_context=context;
	image_timeout_id=setTimeout("checkWindow();",99);
}

//-----------------
var video_context;
var video_timeout_id;
function checkVideoWindow(){
	var video_code=getCookie('videocookie');
	if (!video_code) {
		clearTimeout(video_timeout_id);
		video_timeout_id=setTimeout("checkVideoWindow();",99);
		return;
	}

	deleteCookie('videocookie');
	clearTimeout(video_timeout_id);

	var f = document.forms[video_context];
	if (f) {
		m=f.message.value += video_code;
		return;
	}

	var user_context_element = document.getElementById(video_context);
	if (user_context_element) {
		user_context_element.value += video_code;
	}

}
function addVideo(context){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=780,height=466";
	if(window.screen){
		var xc=screen.availWidth/2-390;
		var yc=screen.availHeight/2-233;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('videocookie');
	window.open("http://"+document.location.hostname+"/p/add_video.cgi","",str);
	video_context=context;
	video_timeout_id=setTimeout("checkVideoWindow();",99);
}

//------------------------
var file_context;
var file_timeout_id;
function checkWindow2(){
	var image_code=getCookie('filecookie');
	if (image_code) {
		var f = document.forms[file_context];
		if (f) {
			if (image_code){
				m=f.message.value += image_code;
			}
		}
		deleteCookie('filecookie');
		clearTimeout(file_timeout_id);
	}
	else{
		clearTimeout(file_timeout_id);
		file_timeout_id=setTimeout("checkWindow2();",99);
	}
}
function addFile(context){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=780,height=466";
	if(window.screen){
		var xc=screen.availWidth/2-390;
		var yc=screen.availHeight/2-233;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('filecookie');
	window.open('http://web.archive.org/web/20070705045904/http://beon.ru/p/add_file.cgi','',str);
	file_context=context;
	file_timeout_id=setTimeout("checkWindow2();",99);
}
var user_context;
var user_timeout_id;
function checkWindow3(){
	var user_code=getCookie('usercookie');
	if (!user_code) {
		clearTimeout(user_timeout_id);
		user_timeout_id=setTimeout("checkWindow3();",99);
		return;
	}

	deleteCookie('usercookie');
	clearTimeout(user_timeout_id);

	var f = document.forms[user_context];
	if (f) {
		m=f.message.value += user_code;
		return;
	}
	var user_context_element = document.getElementById(user_context);
	if (user_context_element) {
		user_context_element.value += user_code;
	}
}
function addUser(context){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=780,height=466";
	if(window.screen){
		var xc=screen.availWidth/2-390;
		var yc=screen.availHeight/2-233;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('usercookie');
	window.open('http://web.archive.org/web/20070705045904/http://beon.ru/p/add_user.cgi?topic_id='+last_topic_id+'&blog_topic_id='+last_blog_topic_id,'',str);
	user_context=context;
	user_timeout_id=setTimeout("checkWindow3();",99);
}
var blog_context;
var blog_timeout_id;
function checkWindow4(){
	var blog_code=getCookie('blogcookie');
	if (!blog_code) {
		clearTimeout(blog_timeout_id);
		blog_timeout_id=setTimeout("checkWindow4();",99);
		return;
	}

	deleteCookie('blogcookie');
	clearTimeout(blog_timeout_id);

	var f = document.forms[blog_context];
	if (f) {
		m=f.message.value += blog_code;
		return;
	}
	var user_context_element = document.getElementById(blog_context);
	if (user_context_element) {
		user_context_element.value += blog_code;
	}
}
function addBlog(context){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=780,height=466";
	if(window.screen){
		var xc=screen.availWidth/2-390;
		var yc=screen.availHeight/2-233;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('blogcookie');
	window.open('http://web.archive.org/web/20070705045904/http://beon.ru/p/add_blog.cgi?topic_id='+last_topic_id+'&blog_topic_id='+last_blog_topic_id,'',str);
	blog_context=context;
	blog_timeout_id=setTimeout("checkWindow4();",99);
}
function addSmile(context,smile){
	var m;
	var f=document.forms[context];
	if(f){
		m = f.message;
	}else {
		m = document.getElementById(context);
	}

	if(!m) {
		return;
	}

	if(document.selection){
		m.focus();
		sel=document.selection.createRange();
		sel.text=smile;
	}else if( m.selectionStart || m.selectionStart=="0" ) {
		var s=m.selectionStart;
		var e=m.selectionEnd;
		m.value=m.value.substring(0,s)+smile+m.value.substring(e,m.value.length);
	}else{
		m.value += smile;
	}
}
function Resize(context, amount) {
	var m;
	var f = document.forms[context];
	if (f) {
		m = f.message;
	}else {
		m = document.getElementById(context);
	}

	if (!m) {
		return;
	}

	var newheight = parseInt(m.style.height, 10) + amount;
	if (newheight < 280) {
		newheight = 280;
	}
	m.style.height = newheight + 'px';
}
function addTag(context,tag){
	var f = document.forms[context];
	if(f){
		var m = f.message;
	}else {
		var m=document.getElementById(context);
	}

	if(!m) {
		return;
	}

	if(document.selection){
		m.focus();
		sel=document.selection.createRange();
		sel.text='['+tag+']'+sel.text+'[/'+tag+']';
	}else if(m.selectionStart || m.selectionStart=="0") {
		var s=m.selectionStart;
		var e=m.selectionEnd;
		m.value=m.value.substring(0,s)+'['+tag+']'+m.value.substring(s, e)+'[/'+tag+']'+m.value.substring(e,m.value.length);
	}else{
		m.value += '['+tag+'][/'+tag+']';
	}
}
function clearFormating(context){
	var f=document.forms[context];
	if(f){
		var m=f.message;
	}else {
		var m=document.getElementById(context);
	}

	if(!m) {
		return;
	}

	var str=String(m.value);
	str=str.replace(/\[[BIUSH]\]/ig, '');
	str=str.replace(/\[\/[BIUSH]\]/ig, '');
	str=str.replace(/\[OFF\]/ig, '');
	str=str.replace(/\[\/OFF\]/ig, '');
	str=str.replace(/\[CENTER\]/ig, '');
	str=str.replace(/\[\/CENTER\]/ig, '');
	str=str.replace(/\[RIGHT\]/ig, '');
	str=str.replace(/\[\/RIGHT\]/ig, '');
	str=str.replace(/\[SPOILER\]/ig, '');
	str=str.replace(/\[\/SPOILER\]/ig, '');
	m.value=str;
}

var quoteText = '';
var helped=0;
var pasted=0;
function get_quote() {
	var isMozilla = document.getElementById && !( document.all && document.all.item) && !window.opera;

	if(window.getSelection){
		quoteText = window.getSelection();
		if( isMozilla)
			quoteText = quoteText.toString();
	} else if(document.getSelection){
		quoteText = document.getSelection();
	} else if(document.selection){
		quoteText = document.selection.createRange().text;
	}
}
function quote(context) {
	var text = quoteText;

	if( ! ( window.getSelection || document.getSelection || document.selection)) {
		alert("Ваш браузер не поддерживает автоматическое цитирование. Но Вы можете выделить цитаты в тексте сообщения сами, поставив знак &gt; в начале каждого цитируемого абзаца.");
		return;
	}

	if(text==''){
		if(helped!=1 && pasted!=1){
			helped=1;
			alert("Если Вы хотите вставить цитату, то выделите её и нажмите 'Quote'");
		}
	}else{
		pasted=1;
		var str=String('\n'+text);
		str=str.replace(/(\n\s*)+/g, '\n> ');

		var f=document.forms[context];
		if(f){
			var m=f.message;
			m.focus();
			m.value=m.value+str;
			m.caretPos=m.value;
			m.focus();
		}
	}
}
var rus_lr2 = ('Е-е-О-о-Ё-Ё-Ё-Ё-Ж-Ж-Ч-Ч-Ш-Ш-Щ-Щ-Ъ-Ь-Э-Э-Ю-Ю-Я-Я-Я-Я-ё-ё-ж-ч-ш-щ-э-ю-я-я').split('-');
var lat_lr2 = ('/E-/e-/O-/o-ЫO-Ыo-ЙO-Йo-ЗH-Зh-ЦH-Цh-СH-Сh-ШH-Шh-ъ'+String.fromCharCode(35)+'-ь'+String.fromCharCode(39)+'-ЙE-Йe-ЙU-Йu-ЙA-Йa-ЫA-Ыa-ыo-йo-зh-цh-сh-шh-йe-йu-йa-ыa').split('-');
var rus_lr1 = ('А-Б-В-Г-Д-Е-З-И-Й-К-Л-М-Н-О-П-Р-С-Т-У-Ф-Х-Х-Ц-Щ-Ы-Я-а-б-в-г-д-е-з-и-й-к-л-м-н-о-п-р-с-т-у-ф-х-х-ц-щ-ъ-ы-ь-я').split('-');
var lat_lr1 = ('A-B-V-G-D-E-Z-I-J-K-L-M-N-O-P-R-S-T-U-F-H-X-C-W-Y-Q-a-b-v-g-d-e-z-i-j-k-l-m-n-o-p-r-s-t-u-f-h-x-c-w-'+String.fromCharCode(35)+'-y-'+String.fromCharCode(39)+'-q').split('-');
var pause=false;
function trans(vorTxt,txt){
	var buk=vorTxt+txt;
	var code=txt.charCodeAt(0);

	if(txt=="[")pause=true;
	else if(txt=="]")pause=false;

	if (pause==true|| !(((code>=65)&&(code<=123))||(code==35)||(code==39))) return buk;

	for (x=0;x<lat_lr2.length;x++){
		if (lat_lr2[x]==buk)return rus_lr2[x];
	}
	for (x=0;x<lat_lr1.length;x++){
		if (lat_lr1[x]==txt)return vorTxt+rus_lr1[x];
	}
	return buk;
}
function lat2win(str){
	var strnew=trans("",str.substr(0,1));
	var symb="";
	for (i=1;i<str.length;i++){
		symb=trans(strnew.substr(strnew.length-1,1),str.substr(i,1));
		strnew=strnew.substr(0,strnew.length-1)+symb;
	}
	return strnew;
}
function translit(context){
	var f=document.forms[context];
	if(f){
		var m=f.message;
	}else {
		var m = document.getElementById(context);
	}

	if(!m) {
		return;
	}

	if(document.selection){
		m.focus();
		sel=document.selection.createRange();
		sel.text=lat2win(sel.text);
	}
	else if(m.selectionStart || m.selectionStart=="0") {
		var s=m.selectionStart;
		var e=m.selectionEnd;
		m.value=m.value.substring(0,s)+lat2win(m.value.substring(s, e))+m.value.substring(e,m.value.length);
	}else{
		m.value=lat2win(m.value);
	}
}

var lastDiv='comment_reply';
var xIE4Up,xMac,xUA=navigator.userAgent.toLowerCase();
if (window.opera){
}
else if (document.all && xUA.indexOf('msie')!=-1) {
	xIE4Up=parseInt(navigator.appVersion)>=4;
}
xMac=xUA.indexOf('mac')!=-1;

function xGetElementById(e) {
	if(typeof(e)!='string') return e;
	if(document.getElementById) e=document.getElementById(e);
	else if(document.all) e=document.all[e];
	else e=null;
	return e;
}
function xParent(e,bNode){
	if (!(e=xGetElementById(e))) return null;
	var p=null;
	if (!bNode && xDef(e.offsetParent)) p=e.offsetParent;
	else if (xDef(e.parentNode)) p=e.parentNode;
	else if (xDef(e.parentElement)) p=e.parentElement;
	return p;
}
function xDef() {
	for(var i=0; i<arguments.length; ++i){if(typeof(arguments[i])=='undefined') return false;}
	return true;
}
function reply(topic_id, div_id, form_action, redir_uri,global_topic_id,global_blog_topic_id) {
	// Mac IE 5.x does not like dealing with
	// nextSibling since it does not support it
	if (xIE4Up && xMac) { return true;}

	last_topic_id=global_topic_id;
	last_blog_topic_id=global_blog_topic_id;

	document.forms['comment_form'].topic_id.value		= topic_id;

	var comment_form = xGetElementById('comment_form');
	comment_form.action = form_action;
	comment_form.elements['r'].value = redir_uri;

	var qr_div = xGetElementById('comment_reply');
	var cur_div = xGetElementById(div_id);

	if (lastDiv == 'comment_reply') {
		if (! showQRdiv(qr_div)) {
			return true;
		}
		// Only one swap
		if (! swapnodes(qr_div, cur_div)) {
			return true;
		}
	}
	else if (lastDiv != div_id) {
		var last_div = xGetElementById(lastDiv);
		// Two swaps
		if (! (swapnodes(last_div, cur_div) && swapnodes(qr_div, last_div))) {
			return true;
		}
	}
	lastDiv = div_id;
	// So it does not follow the link
	return false;
}
function swapnodes (orig, to_swap) {
	var orig_pn = xParent(orig, true);
	var next_sibling = orig.nextSibling;
	var to_swap_pn = xParent(to_swap, true);
	if (! to_swap_pn) {
		return false;
	}

	to_swap_pn.replaceChild(orig, to_swap);
	orig_pn.insertBefore(to_swap, next_sibling);
	return true;
}
function showQRdiv(qr_div) {
	if (! qr_div) {
		qr_div = xGetElementById('comment_reply');
		if (! qr_div) {
			return false;
		}
	}
	else if (qr_div.style && xDef(qr_div.style.display)) {
		qr_div.style.display='inline';
		return true;
	}
	else {
		return false;
	}
}
function showOptions(t) { 
	t.nextSibling.style.display = 'block';
	t.style.display='none';
	return false;
}
function showCommentOptionsAdv(t, comment_id, pQuote, pEmail, pEdit, pDelete, pBlock, pApprove) {
	var result_text = new String();
	var added_imgs = new Array();
	if(pQuote) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/smiles/memorize.gif width=17 height=12 border=0 alt="в цитатник" title="добавить в цитатник">&nbsp;<a href=http://beon.ru/p/add_to_quotations.cgi?comment_id='+comment_id+' target=_blank onClick="return ShowResponse(this);" title="Добавит комментарий в цитатник">в&nbsp;цитатник</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pEmail) {
		result_text += '<font class=m3><nobr><img id="emailitimg'+comment_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить комментарий знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://beon.ru/p/email_it.cgi?comment_id='+comment_id+' target=_blank title="Отправить комментарий знакомым на e-mail">отправить&nbsp;на&nbsp;email</a></nobr></font>&nbsp;&nbsp; ';
		added_imgs.push('emailitimg'+comment_id);
	}

	if(pEdit) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/edit.gif width=12 height=12 border=0 alt="Отредактировать комментарий" title="Отредактировать комментарий">&nbsp;<a href=http://beon.ru/p/edit_comment.cgi?comment_id='+comment_id+' title="Отредактировать комментарий">редактировать</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pDelete) {
		result_text += '<font class=m3><nobr><img id="deleteimg'+comment_id+'" class=flag src=http://beon.ru/i/delete.png width=13 height=12 border=0 alt="Удалить комментарий" title="Удалить комментарий">&nbsp;<a href=http://beon.ru/p/delete_comment.cgi?comment_id='+comment_id+' title="Удалить комментарий" target=_blank onClick="return ShowResponse(this);">удалить</a></nobr></font>&nbsp;&nbsp; ';
			added_imgs.push('deleteimg'+comment_id);
	}
	if(pBlock) {
		result_text += '<font class=m3>Банить&nbsp;по&nbsp;<a href=http://beon.ru/p/block.cgi?type=cookie&comment_id='+comment_id+' target=_blank onClick="return ShowResponse(this);">куке</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=ip&comment_id='+comment_id+' target=_blank onClick="return ShowResponse(this);">IP</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=subnet&comment_id='+comment_id+' target=_blank onClick="return ShowResponse(this);">подсети</a></font>&nbsp;&nbsp; ';
	}
	if(pApprove) {
		result_text += '<font class=m3><a href=http://beon.ru/p/approve_comment.cgi?comment_id='+comment_id+' target=_blank onClick="return ShowResponse(this);">Опубликовать</a></font>&nbsp;&nbsp; ';
	}

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function checkPNGVersion() {
	if(!navigator.appVersion.indexOf('MSIE') || window.opera) {
		return false;
	}
	var arVersion = navigator.appVersion.split("MSIE");
	var version = parseFloat(arVersion[1]);

	if(version < 5.5 || version >= 7 || !document.body.filters) {
		return false;
	}
	return true;
}

function fix_png_images(imgarray) {
	if(!checkPNGVersion()) {
		return;
	}

	var iml = imgarray.length;
	for(var i = 0; i < iml; i++) {
		var img = document.getElementById(imgarray[i]);
		FixPNG(img);
	}
}

function fix_all_png_images() {
	if(!checkPNGVersion()) {
		return;
	}

	var im = document.images;
	var iml = im.length;
	for(var i = 0; i < iml; i++) {
		var img = im[i];
		FixPNG(img);
	}
}

function FixPNG(img) {
	if(!img) {
		return;
	}
	var imgsrc = img.src;

	if(typeof(PNGImages) == 'undefined') {
		return;
	}

	if(imgsrc in PNGImages) {
		img.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'"+imgsrc+"\', sizingMethod='scale');";
		img.src = 'http://web.archive.org/web/20070705045904/http://beon.ru/i/emp.gif';
	}
}

function showBlogCommentOptionsAdv(t, blog_host, comment_id, pQuote, pEmail, pEdit, pDelete, pApprove) {
	var result_text = new String();
	var added_imgs = new Array();
	if(pQuote) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/smiles/memorize.gif width=17 height=12 border=0 alt="в цитатник" title="добавить в цитатник">&nbsp;<a href=http://' + blog_host + '/p/add_to_quotations.cgi?comment_id=' + comment_id + ' target=_blank onClick="return ShowResponse(this);" title="Добавит запись в цитатник">в&nbsp;цитатник</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pEmail) {
		result_text += '<font class=m3><nobr><img id="emailitimg'+comment_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить комментарий знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://' + blog_host + '/p/email_it.cgi?comment_id=' + comment_id + ' target=_blank title="отправить комментарий знакомым на e-mail">отправить&nbsp;на&nbsp;email</a></nobr></font>&nbsp;&nbsp; ';
		added_imgs.push('emailitimg'+comment_id);
	}

	if(pEdit) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/edit.gif width=12 height=12 border=0 alt="Отредактировать комментарий" title="Отредактировать комментарий">&nbsp;<a href=http://' + blog_host + '/p/edit_comment.cgi?comment_id=' + comment_id + ' title="Отредактировать комментарий">редактировать</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pDelete) {
		result_text += '<font class=m3><nobr><img id="deleteimg'+comment_id+'" class=flag src=http://beon.ru/i/delete.png width=13 height=12 border=0 alt="Удалить комментарий" title="Удалить комментарий">&nbsp;<a href=http://' + blog_host + '/p/delete_comment.cgi?comment_id=' + comment_id + ' title="Удалить комментарий" target=_blank onClick="return ShowResponse(this);">удалить</a></nobr></font>&nbsp;&nbsp; ';
			added_imgs.push('deleteimg'+comment_id);
	}
	if(pApprove) {
		result_text += '<font class=m3><a href=http://' + blog_host + '/p/approve_comment.cgi?comment_id=' + comment_id + ' target=_blank onClick="return ShowResponse(this);">Опубликовать</a></font>&nbsp;&nbsp; ';
	}

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function showTopicOptionsAdv(t, topic_id, topic_url, enc_topic_name, topic_name, pQuote, pFavorite, pSubscribe, pEmail, pEdit, pDelete, pSocial, pApprove, pBlock, pWait, pAproveAll) {
	var result_text = new String();
	var added_imgs = new Array();
	if(pQuote) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/smiles/memorize.gif width=17 height=12 border=0 alt="в цитатник" title="добавить в цитатник">&nbsp;<a href=http://beon.ru/p/add_to_quotations.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Добавит запись в цитатник">в&nbsp;цитатник</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pFavorite == 1) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/heart.gif width=13 height=12 border=0 alt="в избранное" title="добавить в избранное">&nbsp;<a href=http://beon.ru/p/add_to_favourite.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Добавит тему в избранное">в&nbsp;избранное</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pFavorite == 2) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/heart.gif width=13 height=12 border=0 alt="в избранное" title="добавить в избранное">&nbsp;<a href=http://beon.ru/p/add_to_favourite.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Добавить&nbsp;тему&nbsp;в&nbsp;избранное</a></nobr></font><br>';
	}
	if(pSubscribe == 1) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/email.gif width=13 height=11 border=0 alt="подписаться" title="получать комментарии на e-mail">&nbsp;<a href=http://beon.ru/p/subscribe.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Получать комментарии на e-mail">подписаться</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pSubscribe == 2) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/email.gif width=13 height=11 border=0 alt="подписаться" title="получать комментарии на e-mail">&nbsp;<a href=http://beon.ru/p/subscribe.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Получать комментарии на e-mail">Подписаться&nbsp;на&nbsp;тему</a></nobr></font><br>';
	}
	
	if(pEmail == 1) {
		result_text += '<font class=m3><nobr><img id="emailtopimg'+topic_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить тему знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://beon.ru/p/email_it.cgi?topic_id='+topic_id+' target=_blank title="Отправить тему знакомым на e-mail">отправить&nbsp;на&nbsp;email</a></nobr></font>&nbsp;&nbsp; ';
		added_imgs.push('emailtopimg'+topic_id);
	}
	if(pEmail == 2) {
		result_text += '<font class=m3><nobr><img id="emailtopimg'+topic_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить тему знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://beon.ru/p/email_it.cgi?topic_id='+topic_id+' target=_blank title="Отправить тему знакомым на e-mail">Отправить&nbsp;тему&nbsp;на&nbsp;email</a></nobr></font><br>';
		added_imgs.push('emailtopimg'+topic_id);
	}
	if(pEdit) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/edit.gif width=12 height=12 border=0 alt="Отредактировать топик" title="Отредактировать топик">&nbsp;<a href=http://beon.ru/p/edit_topic.cgi?topic_id='+topic_id+' title="Отредактировать топик">редактировать</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pDelete) {
		result_text += '<font class=m3><nobr><img id="deletetopicimg'+topic_id+'" class=flag src=http://beon.ru/i/delete.png width=13 height=12 border=0 alt="Удалить топик" title="Удалить топик">&nbsp;<a href=http://beon.ru/p/delete_topic.cgi?topic_id='+topic_id+' title="Удалить топик" onClick="return ShowResponse(this);">удалить</a></nobr></font>&nbsp;&nbsp; ';
		added_imgs.push('deletetopicimg'+topic_id);
	}
	if(pSocial) {
		result_text += GetSocialLinks(topic_url,enc_topic_name,topic_name);
	}
	if(pApprove) {
		result_text += '<font class=m3><a href=http://beon.ru/p/approve_topic.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Опубликовать</a></font>&nbsp;&nbsp; ';
	}
	if(pBlock) {
		result_text += '<font class=m3>Банить&nbsp;по&nbsp;<a href=http://beon.ru/p/block.cgi?type=cookie&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">куке</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=ip&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">IP</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=subnet&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">подсети</a></font>&nbsp;&nbsp; ';
	}
	if(pWait > 0 ) {
		result_text += '<font class=m5>'+pWait+' '+CorrectNumber(pWait,'комментарий','комментария','комментариев')+' ожидает модерации</font>&nbsp;&nbsp; ';
	}
	if(pAproveAll) {
		result_text += '<font class=m3><a href=http://beon.ru/p/approve_all_comments.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Опубликовать все комментарии</a></font>&nbsp;&nbsp; ';
	}

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function showBlogTopicOptionsAdv(t, blog_host, topic_id, topic_url, enc_topic_name, topic_name, pQuote, pFavorite, pSubscribe, pEmail, pApprove, pEdit, pDelete, pSocial, pBlock, pWait, pAproveAll) {
	var result_text = new String();
	var added_imgs = new Array();
	if(pQuote) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/smiles/memorize.gif width=17 height=12 border=0 alt="в цитатник" title="добавить в цитатник">&nbsp;<a href=http://'+blog_host+'/p/add_to_quotations.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Добавит запись в цитатник">в&nbsp;цитатник</a></nobr></font>&nbsp;&nbsp; '
	}
	if(pFavorite == 1) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/heart.gif width=13 height=12 border=0 alt="в избранное" title="добавить в избранное">&nbsp;<a href=http://'+blog_host+'/p/add_to_favourite.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Добавит запись в избранное">в&nbsp;избранное</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pFavorite == 2) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/heart.gif width=13 height=12 border=0 alt="в избранное" title="добавить в избранное">&nbsp;<a href=http://'+blog_host+'/p/add_to_favourite.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Добавить&nbsp;запись&nbsp;в&nbsp;избранное</a></nobr></font><br>';
	}
	if(pSubscribe == 1) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/email.gif width=13 height=11 border=0 alt="подписаться" title="получать комментарии на e-mail">&nbsp;<a href=http://'+blog_host+'/p/subscribe.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Получать комментарии на e-mail">подписаться</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pSubscribe == 2) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/email.gif width=13 height=11 border=0 alt="подписаться" title="получать комментарии на e-mail">&nbsp;<a href=http://'+blog_host+'/p/subscribe.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);" title="Получать комментарии на e-mail">Подписаться&nbsp;на&nbsp;комментарии</a></nobr></font><br>';
	}
	
	if(pEmail == 1) {
		result_text += '<font class=m3><nobr><img id="emailtopimg'+topic_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить запись знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://'+blog_host+'/p/email_it.cgi?topic_id='+topic_id+' target=_blank title="Отправить запись знакомым на e-mail">отправить&nbsp;на&nbsp;email</a></nobr></font>&nbsp;&nbsp; '
		added_imgs.push('emailtopimg'+topic_id);
	}
	if(pEmail == 2) {
		result_text += '<font class=m3><nobr><img id="emailtopimg'+topic_id+'" class=flag src=http://beon.ru/i/emailit.png alt="На e-mail" title="Отправить запись знакомым на e-mail" border=0 height=11 width=25>&nbsp;<a href=http://'+blog_host+'/p/email_it.cgi?topic_id='+topic_id+' target=_blank title="Отправить запись знакомым на e-mail">Отправить&nbsp;тему&nbsp;на&nbsp;email</a></nobr></font><br>';
		added_imgs.push('emailtopimg'+topic_id);
	}
	if(pApprove) {
		result_text += '<font class=m3><a href=http://'+blog_host+'/p/approve_topic.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Опубликовать</a></font>&nbsp;&nbsp; ';
	}
	if(pEdit) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/edit.gif width=12 height=12 border=0 alt="Отредактировать запись" title="Отредактировать запись">&nbsp;<a href=http://'+blog_host+'/p/edit_topic.cgi?topic_id='+topic_id+' title="Отредактировать запись">редактировать</a></nobr></font>&nbsp;&nbsp; ';
	}
	if(pDelete) {
		result_text += '<font class=m3><nobr><img id="deletetopicimg'+topic_id+'" class=flag src=http://beon.ru/i/delete.png width=13 height=12 border=0 alt="Удалить запись" title="Удалить запись">&nbsp;<a href=http://'+blog_host+'/p/delete_topic.cgi?topic_id='+topic_id+' title="Удалить запись" onClick="return ShowResponse(this);">удалить</a></nobr></font>&nbsp;&nbsp; ';
		added_imgs.push('deletetopicimg'+topic_id);
	}
	if(pSocial) {
		result_text += GetSocialLinks(topic_url,enc_topic_name,topic_name);
	}
	if(pBlock) {
		result_text += '<font class=m3>Банить&nbsp;по&nbsp;<a href=http://'+blog_host+'/p/block.cgi?type=cookie&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">куке</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=ip&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">IP</a>&nbsp;<a href=http://beon.ru/p/block.cgi?type=subnet&topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">подсети</a></font>&nbsp;&nbsp; ';
	}
	if(pWait > 0 ) {
		result_text += '<font class=m5>'+pWait+' '+CorrectNumber(pWait,'комментарий','комментария','комментариев')+' ожидает модерации</font>&nbsp;&nbsp; ';
	}
	if(pAproveAll) {
		result_text += '<font class=m3><a href=http://'+blog_host+'/p/approve_all_comments.cgi?topic_id='+topic_id+' target=_blank onClick="return ShowResponse(this);">Опубликовать все комментарии</a></font>&nbsp;&nbsp; ';
	}

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function showWishesOptions(t, blogLogin, wishId, copyToUserLogin, pChange, pDelete, pCopy) {
	var result_text = new String();
	var added_imgs = new Array();

	if(pChange) {
		result_text += '<font class=m3><a href="http://' + blogLogin + '.beon.ru/p/add_wish.cgi?wish_id=' + wishId + '"><img class=flag src=http://beon.ru/i/edit.gif alt="редактировать" title="редактировать" border=0 height=12 width=12>&nbsp;редактировать...</a>&nbsp;&nbsp;&nbsp;';
	}

	if(pDelete) {
		result_text += '<a href="/p/delete_wish.cgi?wish_id=' + wishId + '" onClick="return ShowResponse(this);"><img id="wishesDel' + wishId + '" class=flag src=http://beon.ru/i/delete.png alt="удалить" title="удалить" border=0 height=12 width=13>&nbsp;удалить...</a></font>&nbsp;&nbsp;&nbsp;';
		added_imgs.push('wishesDel' + wishId);
	}

	if(pCopy) {
		result_text += '<font class=m3><nobr><img class=flag src=http://beon.ru/i/copy.gif alt="скопировать себе" title="скопировать себе" border=0 height=12 width=15>&nbsp;<a href="http://' + copyToUserLogin + '.beon.ru/p/add_wish.cgi?copy=1&wish_id=' + wishId + '">скопировать себе...</a></nobr></font>';
	}

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function showLinksOptions(t, blogLogin, linkId, pChange) {
	var result_text = new String();
	var added_imgs = new Array();

	if(pChange) {
		result_text += '<font class=m3><a href="http://' + blogLogin + '.beon.ru/p/add_link.cgi?link_id=' + linkId + '"><img class=flag src=http://beon.ru/i/edit.gif alt="редактировать" title="редактировать" border=0 height=12 width=12>&nbsp;редактировать...</a>&nbsp;&nbsp;&nbsp;';
	}

	result_text += '<a href="/p/delete_link.cgi?link_id=' + linkId + '" onClick="return ShowResponse(this);"><img id="linksDel' + linkId + '" class=flag src=http://beon.ru/i/delete.png alt="удалить" title="удалить" border=0 height=12 width=13>&nbsp;удалить...</a></font>&nbsp;&nbsp;&nbsp;';
	added_imgs.push('linksDel' + linkId);

	var node = xParent(t, true);
	if(node) {
		node.innerHTML = result_text;
		fix_png_images(added_imgs);
	}

	return false;
}

function CorrectNumber(n, a, b, c) {
	var u = n % 10;
	var d = (n / 10) % 10;
	if (u == 1 && d != 1) {
		return a;
	}else if (u >= 2 && u < 5 && d != 1) {
		return b;
	}
	return c;
}

function insertUser(context,smile){
	var f=document.forms[context];
	if(f){
		var m=f.message;
		m.value += smile;
	}
}

var podcast_timeout_id;
function checkWindow5(){
	var podcast_url = getCookie('podcast_urlcookie');
	var podcast_name = getCookie('podcast_namecookie');
	if (podcast_url) {
		var f = document.forms['topic_form'];
		if (f) {
			m = f.podcast_url.value = podcast_url;
			m = f.podcast_name.value = podcast_name;
			document.getElementById('podcast').innerHTML='<a href='+podcast_url+' target=_blank>'+podcast_name+'</a>&nbsp;<img id="clearpodcast" class=rad src=http://beon.ru/i/smiles/clear.png width=15 height=15 border=0 alt="Удалить" title="Удалить подкаст" style="cursor:pointer;cursor:hand" onclick="deletePodcast();">&nbsp;&nbsp;&nbsp;<a href=# target=_blank onclick="return addPodcast();">Перезакачать подкаст</a>';
			if(navigator.appVersion.indexOf('MSIE')) {
				var arVersion = navigator.appVersion.split("MSIE");
				var version = parseFloat(arVersion[1]);
				if(version >= 5.5 && version < 7 && document.body.filters) {
					var clrimg = document.getElementById('clearpodcast');
					if(clrimg) {
						var imgsrc = clrimg.src;
						if(imgsrc in PNGImages) {
							clrimg.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'"+imgsrc+"\', sizingMethod='scale');";
							clrimg.src = 'http://web.archive.org/web/20070705045904/http://beon.ru/i/emp.gif';
						}
					}
				}
			}
		}
		deleteCookie('podcast_urlcookie');
		deleteCookie('podcast_namecookie');
		clearTimeout(podcast_timeout_id);
	}
	else{
		clearTimeout(podcast_timeout_id);
		podcast_timeout_id=setTimeout("checkWindow5();",99);
	}
}
function addPodcast(){
	var str="toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=680,height=420";
	if(window.screen){
		var xc=screen.availWidth/2-340;
		var yc=screen.availHeight/2-210;
		str+=",left="+xc+",screenX="+xc+",top="+yc+",screenY="+yc;
	}
	else{
		str+=",top=120";
	}
	deleteCookie('podcast_urlcookie');
	deleteCookie('podcast_namecookie');
	window.open("http://"+document.location.hostname+"/p/add_podcast.cgi","",str);
	podcast_timeout_id=setTimeout("checkWindow5();",99);
	return false;
}
function deletePodcast(){
	var f = document.forms['topic_form'];
	if (f) {
		m=f.podcast_url.value = '';
		m=f.podcast_name.value = '';
		document.getElementById('podcast').innerHTML='<a href=# target=_blank onclick="return addPodcast();">Закачать подкаст</a>';
	}
}
function GetSocialLinks(topic_url_escaped,topic_name_escaped,topic_name){
	var res = '<nobr><a href="http://web.archive.org/web/20070705045904/http://del.icio.us/post?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в del.icio.us: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/delicious.gif alt="del.icio.us" border=0 height=16 width=16 title="Добавить в del.icio.us: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://cgi.fark.com/cgi/fark/edit.pl?new_url='+topic_url_escaped+'&new_comment='+topic_name_escaped+'" title="Запостить в Fark: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/fark.gif alt="Fark" border=0 height=16 width=16 title="Запостить в Fark: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://digg.com/submit?phase=2&url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Запостить в digg: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/digg.gif alt="digg" border=0 height=16 width=16 title="Запостить в digg: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://reddit.com/submit?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Запостить в Reddit: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/reddit.gif alt="Reddit" border=0 height=16 width=16 title="Запостить в Reddit: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://myweb.yahoo.com/myresults/bookmarklet?u='+topic_url_escaped+'&t='+topic_name_escaped+'&ei=UTF8" title="Добавить в MyWeb: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/myweb.gif alt="MyWeb" border=0 height=16 width=16 title="Добавить в MyWeb: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://blogmarks.net/my/new.php?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в BlogMarks: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/blogmarks.gif alt="BlogMarks" border=0 height=16 width=16 title="Добавить в BlogMarks: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://www.furl.net/storeIt.jsp?u='+topic_url_escaped+'&t='+topic_name_escaped+'" title="Добавить в Furl: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/furl.gif alt="Furl" border=0 height=16 width=16 title="Добавить в Furl: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://jots.com/?cmd=do_post&url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в Jots: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/jots.gif alt="Jots" border=0 height=16 width=16 title="Добавить в Jots: '+topic_name+'"></a> <a href="http://web.archive.org/web/20070705045904/http://news2.ru/add_story.php?url='+topic_url_escaped+'" title="Добавить в News2: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/news2.gif alt="News2" border=0 height=16 width=16 title="Добавить в News2: '+topic_name+'"></a></nobr>&nbsp;&nbsp; ';
	return res;
}
function SocialLinks(topic_url_escaped,topic_name_escaped,topic_name){
	document.write('<nobr>');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://del.icio.us/post?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в del.icio.us: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/delicious.gif alt="del.icio.us" border=0 height=16 width=16 title="Добавить в del.icio.us: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://cgi.fark.com/cgi/fark/edit.pl?new_url='+topic_url_escaped+'&new_comment='+topic_name_escaped+'" title="Запостить в Fark: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/fark.gif alt="Fark" border=0 height=16 width=16 title="Запостить в Fark: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://digg.com/submit?phase=2&url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Запостить в digg: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/digg.gif alt="digg" border=0 height=16 width=16 title="Запостить в digg: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://reddit.com/submit?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Запостить в Reddit: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/reddit.gif alt="Reddit" border=0 height=16 width=16 title="Запостить в Reddit: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://myweb.yahoo.com/myresults/bookmarklet?u='+topic_url_escaped+'&t='+topic_name_escaped+'&ei=UTF8" title="Добавить в MyWeb: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/myweb.gif alt="MyWeb" border=0 height=16 width=16 title="Добавить в MyWeb: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://blogmarks.net/my/new.php?url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в BlogMarks: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/blogmarks.gif alt="BlogMarks" border=0 height=16 width=16 title="Добавить в BlogMarks: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://www.furl.net/storeIt.jsp?u='+topic_url_escaped+'&t='+topic_name_escaped+'" title="Добавить в Furl: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/furl.gif alt="Furl" border=0 height=16 width=16 title="Добавить в Furl: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://jots.com/?cmd=do_post&url='+topic_url_escaped+'&title='+topic_name_escaped+'" title="Добавить в Jots: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/jots.gif alt="Jots" border=0 height=16 width=16 title="Добавить в Jots: '+topic_name+'"></a> ');
	document.write('<a href="http://web.archive.org/web/20070705045904/http://news2.ru/add_story.php?url='+topic_url_escaped+'" title="Добавить в News2: '+topic_name+'" target=_blank rel=nofollow><img class=socbtm src=http://beon.ru/i/news2.gif alt="News2" border=0 height=16 width=16 title="Добавить в News2: '+topic_name+'"></a>');
	document.write('</nobr>&nbsp;&nbsp; ');
}

var images_to_check = new Array();
function register_image(id) {
	images_to_check.push(id);
}

var resize_images_timeout;
function check_images() {
	resize_images_timeout = setTimeout(resize_images, 1000);
}

function resize_images() {
	clearTimeout(resize_images_timeout);
	var i = 0;
	var img;
	while(img = document.getElementById('i'+images_to_check[i])) {

		if(!img.complete) {
			i += 1;
			continue;
		}

		var window_width	= document.body.clientWidth;

		var max_img_width = (window_width - 20) * 0.75 - 100;
		if(max_img_width < 400) {
			max_img_width == 400;
		}

		max_img_height = max_img_width * 1.5;

		if(img.width > max_img_width) {
			img.height = img.height / img.width * max_img_width;
			img.width = max_img_width
			img.className = 'zoom';
		}

		if(img.height > max_img_height) {
			img.width = img.width / img.height * max_img_height;
			img.height = max_img_height;
			img.className = 'zoom';
		}

		images_to_check.splice(i, 1);
	}
	if(images_to_check.length > 0) {
		setTimeout(check_images, 1000);
	}
}
//------------------------------------------------------------------------------
// AJAX Class
//------------------------------------------------------------------------------
function Ajax() {
	this.req = null;
	this.url = null;
	this.PostData = null;
	this.Method = null;
	this.status_error = null;
	this.statusText = '';
}
//------------------------------------------------------------------------------
Ajax.prototype.empty_func = function() {};
//------------------------------------------------------------------------------
Ajax.prototype.init = function() {
	if(!this.req) {
		try {
			this.req = new XMLHttpRequest();
		}catch(e) {
			try{
				this.req = new ActiveXObject('MSXML2.XMLHTTP');
			}catch(e) {
				try{
					this.req = new ActiveXObject('Microsoft.XMLHTTP');
				}catch(e) {
					// Could not create an XMLHttpRequest object.
					return false;
				}
			}
		}
	}
	return this.req;
};
//------------------------------------------------------------------------------
Ajax.prototype.doReq = function() {
	if(!this.init()) {
		return;
	}
	var self = this; // Fix loss-of-scope in inner function
	this.req.onreadystatechange = function() {
		var resp = null;
		if(self.req.readyState != 4 || self.req.readyState == 0)
			 return;
		self.req.onreadystatechange = self.empty_func;

		try {
			if(self.req.status != 200) {
				self.status_error(self.req.status);
				return;
			}
		}catch(e) {
			self.status_error(500); // well, this was most logical status
			return;
		}
		self.status_ok(self.req.responseText);
	};
	this.req.open(this.Method, this.url, true);
	if(this.Method == 'POST') {
		this.req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
	}
	this.req.send(this.PostData || '');
	return 1;
};
//------------------------------------------------------------------------------
Ajax.prototype.status_error	= function(a) {};
//------------------------------------------------------------------------------
Ajax.prototype.status_ok		= function(a) {};
//------------------------------------------------------------------------------
Ajax.prototype.doGet = function(url, f_ok, f_error) {
	this.url			= url;
	this.status_error	= f_error;
	this.status_ok		= f_ok;
	this.Method			= 'GET';
	this.doReq();
};
//------------------------------------------------------------------------------
Ajax.prototype.doPost = function(url, data, f_ok, f_error) {
	this.url			= url;
	this.PostData		= data;
	this.status_error	= f_error;
	this.status_ok		= f_ok;
	this.Method			= 'POST';

	this.doReq();
};
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
// END of AJAX Class
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
var func_err	= function(a) {};
//------------------------------------------------------------------------------
function showTagsCloud() {
	if(xGetElementById('add_tags_div')) {

		var tags_div = xGetElementById('add_tags_div');
		tags_div.style.display = 'none';

		setTimeout("var tdiv=xGetElementById('add_tags_div');if(tdiv) xParent(tdiv, true).removeChild(tdiv);", 100);
		return false;
	}
	var ajax = new Ajax();
	ajax.doGet('http://'+document.location.hostname+'/ajax/gen_tags', CalculateTags, func_err);
	return false;
}
//------------------------------------------------------------------------------
var categories = null;
var hov_timeout;
function CalculateTags(resp) {
	var str_tags_array = resp.split(/,/);

	if(str_tags_array.length % 2) { // должно быть четное количество элементов
		return false;
	}

	var i = 0;
	var cnt = 0;
	var result_str = new String();
	categories = new Array();
	while(str_tags_array.length > i) {
		var tag_name = str_tags_array[i++];
		var tag_size = str_tags_array[i++];

		var quoted_name = tag_name.replace(/\\/g, '\\\\');
		var quoted_name = tag_name.replace(/\'/g, '\\\'');
		categories.push(tag_name);

		result_str += ' <a style="font-size: ' + tag_size + 'px;" class=tag href=# onclick="return addTagName(\'' + cnt + '\')"/>' + fix_tag_to_html(tag_name ) + '</a>';
		cnt++;
	}

	clearTimeout(hov_timeout);

	var tag_div = xGetElementById('add_tags_div');
	if(!tag_div) {
		tag_div  = document.createElement('DIV');
		var comment_link_element = xGetElementById('add_comment_link');
		xParent(comment_link_element,true).insertBefore(tag_div, comment_link_element);
		tag_div.id = "add_tags_div";
		var offset = get_offset(comment_link_element);
		tag_div.style.left = offset.left;
		tag_div.style.top = offset.top + 12;
	}

	tag_div.innerHTML = result_str;

	if (tag_div.addEventListener) {
		tag_div.addEventListener("mouseover",	do_hover,  false);
		tag_div.addEventListener("mouseout",	out_hover, false);
	} else if (tag_div.attachEvent) {
		tag_div.attachEvent("onmouseover",	do_hover);
		tag_div.attachEvent("onmouseout",	out_hover);
	} else {
		tag_div.onmouseover	= do_hover;
		tag_div.onmouseout	= out_hover;
	}

	out_hover();

	return false;
}
//------------------------------------------------------------------------------
function out_hover() {
	hov_timeout = setTimeout("clearTimeout(hov_timeout);var tdiv = xGetElementById('add_tags_div');if(tdiv) {tdiv.style.display='none';hov_timeout = setTimeout(\"var tdiv = xGetElementById('add_tags_div');if(tdiv) xParent(tdiv, true).removeChild(tdiv)\", 100);}", 2000);
}
//------------------------------------------------------------------------------
function do_hover() {
	clearTimeout(hov_timeout);
}
//------------------------------------------------------------------------------
function addTagName(tag_num) {
	var tag_name = categories[tag_num];
	var input_tags = xGetElementById('tags');
	input_tags.focus();
	value = input_tags.value;
	value = value.replace(/^\s+/, '');
	value = value.replace(/\s+$/, '');
	value = value.replace(/,+/g, ',');
	value = value.replace(/,$/,   '');
	if(value.length > 0) {
		value += ',';
	}
	tag_name = tag_name;
	value += tag_name + ',';
	input_tags.value = value;
	input_tags.focus();
	return false;
}
//------------------------------------------------------------------------------
function get_offset(element) {
	var left = element.offsetLeft;
	var top = element.offsetTop;
	for (var parent = element.offsetParent; parent; parent = parent.offsetParent)	{
		left += parent.offsetLeft;
		top += parent.offsetTop;
	}
	var right = left + element.offsetWidth;
	var bottom = top + element.offsetHeight;
	return {left: left, top: top, right: right, bottom: bottom};
}
//------------------------------------------------------------------------------
var searchArray = new Array();
function ParseTagSuggestion(resp) {
	var str_tags_array = resp.split(/,/);
	if(str_tags_array.length % 2) { // должно быть четное количество элементов
		return false;
	}

	var i = 0;
	categories = new Array();
	while(str_tags_array.length > i) {
		var tag_name = str_tags_array[i++];
		var tag_size = str_tags_array[i++];
		categories.push(tag_name);
	}

	ShowTagSuggestion();
};
//------------------------------------------------------------------------------
function ShowTagSuggestion() {
	var input_element = xGetElementById('tags');
	var element_value = input_element.value;

	var str = element_value.match(/,[^,]+$/g);
	if(str != null && typeof(str) == 'object') {
		str = str[0];
	}
	if(str == null && element_value.length > 0) {
		str = element_value;
	}

	if(str == null) {
		CloseSuggestBox();
		return;
	}else {
		str = str.replace(/\s+$/, '');
		if(str.match(/,$/)) {
			CloseSuggestBox();
			return;
		}

		str = str.replace(/^,/, '');
		str = str.replace(/,$/, '');
		ShowSuggestBox(str, element_value);
	}
}
//------------------------------------------------------------------------------
var is_suggest_active = false;
function CloseSuggestBox() {
	var suggest_div = xGetElementById('suggest_div');
	if(suggest_div) {
		suggest_div.style.display = 'none';
		setTimeout("var sdiv = xGetElementById('suggest_div');if(sdiv) xParent(sdiv, true).removeChild(sdiv)", 100);
		is_suggest_active = false;
	}
}
//------------------------------------------------------------------------------
function tagContains(tag_name, tags_array) {
	var u_tag_name = tag_name.toUpperCase();
	for(var j = 0; j < tags_array.length; j++) {
		if(tags_array[j].toUpperCase() == u_tag_name)
			return true;
	}
	return false;
}
//------------------------------------------------------------------------------
var regEx = new RegExp();
function ShowSuggestBox(tag_start, tags_str) {
	tag_start = tag_start.replace(/^\s+/, '');
	tag_start = tag_start.replace(/\s+$/, '');

	regEx.compile("^".tag_start, "i");

	var tags_array = tags_str.split(/\s*,\s*/);

	var result = '';
	searchArray = new Array();
	iSelect = -1;
	var counter = 0;
	for(var i = 0; i < categories.length; i++) {
//		if(regEx.test(categories[i])) {
		var curr_tag = categories[i];
		var curr_tag_upper = curr_tag.toUpperCase();
		if(curr_tag_upper.indexOf(tag_start.toUpperCase()) == 0 && 
				curr_tag_upper != tag_start.toUpperCase() && 
				!tagContains(curr_tag, tags_array)) {
			result += '<a href="#" id="a_id'+ counter +
					'" onmouseover="moveTo('+ counter +
					')" class="notactive" onclick="return addSuggest(\'' + counter + '\')">' + 
					fix_tag_to_html(categories[i]) + '</a>';
			searchArray.push(categories[i]);
			counter++;
		}
	}

	if(result == '') {
		CloseSuggestBox();
		return;
	}

	var suggest_div = xGetElementById('suggest_div');
	if(!suggest_div) {
		var suggest_div = document.createElement('DIV');
		var tags_element = xGetElementById('tags');
		xParent(tags_element,true).insertBefore(suggest_div, tags_element);
		suggest_div.id = "suggest_div";

		var offset = get_offset(tags_element);
		suggest_div.style.left	= offset.left;
		suggest_div.style.top	= offset.top + 21;
		is_suggest_active = true;
	}

	suggest_div.innerHTML = result;
}
//------------------------------------------------------------------------------
function addSuggest(index) {

	var tag_name = searchArray[index];

	var input_element = xGetElementById('tags');
	input_element.focus();
	var element_value = input_element.value;
	element_value = element_value.replace(/,+/g, ',');

	if(element_value.indexOf(',') >= 0) {
		element_value = element_value.replace(/,[^,]*$/, ','+tag_name+',');
	}else {
		element_value = tag_name+',';
	}

	CloseSuggestBox();

	input_element.value = element_value;
	input_element.focus();
	searchArray = new Array();
	categories = null;
	return false;
}
//------------------------------------------------------------------------------
var pressedEnter = false;
function break_enter_key(e) {
	if(window.event)	{
		e = window.event;
		k = e.keyCode;
		s = e.srcElement 
	}else if(e)	{
		k = e.keyCode;
		s = e.target
	}

	if(k == 13) { // catch Enter key to prevent submit
		pressedEnter = true;
	}
}
//------------------------------------------------------------------------------
function block_submit(elem, mode, user_login) {

	if(is_suggest_active && pressedEnter) {

		addSuggest(iSelect);
		return false;
	}

	var url = '';
	if(user_login.length > 0) {
		url = 'http://'+user_login+'.beon.ru';
	}
	if(mode == 'add') {
		url += '/p/add_topic.cgi';
	}else {
		url += '/p/edit_topic.cgi';
	}
	elem.action = url;

	return true;
}
//------------------------------------------------------------------------------
function sugest_category(e) {
	if(window.event)	{
		e = window.event;
		k = e.keyCode;
		s = e.srcElement 
	}else if(e)	{
		k = e.keyCode;
		s = e.target
	}

	if(!k || !s)
		return true;
	
	if(k == 38) {
		moveUp();
		return true;
	}
	
	if(k == 40)	{
		moveDown();
		return true;
	}

	if(categories == null) {
		categories = new Array();
		var ajax = new Ajax();
		ajax.doGet('http://'+document.location.hostname+'/ajax/gen_tags', ParseTagSuggestion, func_err);
		return true;
	}

	if(categories.length == 0) {
		return true;
	}

	ShowTagSuggestion();
}
//------------------------------------------------------------------------------
var iSelect=-1;
function moveUp(){
	var k = 0;
	if(!searchArray.length) {
		iSelect = -1;
		return false
	};
	k = (iSelect <= 0) ? searchArray.length - 1 : iSelect - 1;
	moveTo(k);
}
//------------------------------------------------------------------------------
function moveDown(){
	var k = 0;
	if (!searchArray.length) {
		iSelect = -1;
		return false
	};
	k = (iSelect < searchArray.length - 1) ? iSelect + 1 : 0;
	moveTo(k);
}
//------------------------------------------------------------------------------
function moveTo(k){
	var tmpObj;
	if(tmpObj = xGetElementById("a_id"+iSelect)) 
		tmpObj.className = "notactive";

	iSelect = k;

	if(tmpObj = xGetElementById("a_id"+iSelect))
		tmpObj.className = "active";
	TryTag(k);
}
//------------------------------------------------------------------------------
function TryTag(k) {
	if(!searchArray[k])
		return;
	var value		= xGetElementById('tags').value;
	value = value.replace(/^\s+/, '');
	value = value.replace(/\s+$/, '');
	value = value.replace(/,+/g, ',');
	if(value.indexOf(',') >= 0) {
		value = value.replace(/,[^,]+$/,   ',');
	}else {
		value = '';
	}

	xGetElementById('tags').value = value + searchArray[k];
	return;
}
//------------------------------------------------------------------------------
function fix_tag_to_html(tag_name) {
	tag_name = tag_name.replace(/\&/g, '&amp;');
	tag_name = tag_name.replace(/>/g, '&gt;');
	tag_name = tag_name.replace(/</g, '&lt;');
	tag_name = tag_name.replace(/"/g, '&quot;');
	tag_name = tag_name.replace(/'/g, "&apos;");
	return tag_name;
}
//------------------------------------------------------------------------------
var updater_timeout_obj;
var current_topic_id;
var last_comment_id;
var is_blog;
function StartCommentUpdater(p_topic_id, p_last_comment, p_is_blog) {
	current_topic_id	= p_topic_id;
	last_comment_id		= p_last_comment;
	is_blog				= p_is_blog;

	// Start updater
	runChecker();
}
//------------------------------------------------------------------------------
var is_runned;
function runChecker() {
	if(is_runned != undefined) {
		return;
	}
	is_runned = 1;
	updater_timeout_obj = setTimeout("checkNewComments();", 5000);
}
//------------------------------------------------------------------------------
var func_err_run_again	= function(a) {
	is_runned = undefined;
	runChecker();
};
//------------------------------------------------------------------------------
function checkNewComments() {
	clearTimeout(updater_timeout_obj);
	if(is_runned != 1) {
		return;
	}
	is_runned = 2;
	var ajax = new Ajax();
	var str_length = String(current_topic_id).length;

	var first_dir;
	if(str_length >= 2) {
		first_dir = String(current_topic_id).substr(str_length - 2);
	}else {
		first_dir = String(current_topic_id);
	}

	var second_dir;
	if(str_length >= 4) {
		second_dir = String(current_topic_id).substr(str_length - 4, 2);
	}else if(str_length == 3) {
		second_dir = String(current_topic_id).substr(0, 1);
	}else {
		second_dir = String(current_topic_id);
	}

	var url = 'http://'+document.location.hostname+'/i/'+(is_blog?'b':'t')+'/'+first_dir+'/'+second_dir+'/'+current_topic_id+'.txt?' + Math.random();
	ajax.doGet(url, CheckLastTopicID, func_err_run_again);

	return true;
}
//------------------------------------------------------------------------------
var func_err_comm	= function(a) {
	is_runned = undefined;
};
//------------------------------------------------------------------------------
function CheckLastTopicID(resp) {
	if(is_runned != 2)	{
		return;
	}
	is_runned = 3

	resp = resp.replace(/^\s+/, '');
	resp = resp.replace(/^\n+/, '');
	resp = resp.replace(/\s+$/, '');
	resp = resp.replace(/\n+$/, '');

	if(!resp.match(/^\d+$/)) {
		is_runned = undefined;
		runChecker();
		return;
	}

	if(last_comment_id >= resp) {
		is_runned = undefined;
		runChecker();
		return;
	}

	GetNewComments();
}
//------------------------------------------------------------------------------
function GetNewComments(resp) {
	if(is_runned != 3)	{
		return;
	}
	is_runned = 4
	var hide = false

	if(resp) {
		if(resp.match(/^HIDE/)) {
			resp = resp.replace(/^HIDE/, '');
			hide = true;
		}

		if(resp.match(/^Ошибка/)) {
			var err_div			= xGetElementById('error_message');
			err_div.innerHTML	= '<font color=ff0000>'+resp+'</font>';
			err_div.style.display = 'block';
			is_runned = undefined;
			runChecker();
			return;
		}
		var form_elem = xGetElementById('comment_form');
		if(resp.match(/^REDIR/)) {
			form_elem.submit();
		}
		form_elem.message.value ='';
		if(hide) {
			HideFormIfCan();
		}
	}
	is_runned = 2;

	var ajax = new Ajax();
	var url = 'http://'+document.location.hostname+'/ajax/get_comments?tid='+current_topic_id+'&lcid='+last_comment_id;
	ajax.doGet(url, UpdateComments, func_err_comm);
}
//------------------------------------------------------------------------------
function UpdateComments(resp) {
	is_runned = undefined;

	if(resp.match(/^Ошибка/)) {
		var err_div			= xGetElementById('error_message');
		err_div.innerHTML	= '<font color=ff0000>'+resp+'</font>';
		err_div.style.display = 'block';
		is_runned = undefined;
		runChecker();
		return;
	}

	try {
		eval(resp);
	}catch(e) {
		runChecker();
		return;
	}
	resize_images_timeout = setTimeout(fix_comment, 1000);
	runChecker();
};
//------------------------------------------------------------------------------
function AddComment(p_html_text) {
	var container = xGetElementById('comments_box');
	var elem = document.createElement("SPAN");
	elem.innerHTML = p_html_text;
	container.appendChild(elem);

	fix_all_png_images();
}
//------------------------------------------------------------------------------
function setLastComment(p_last_comment_id) {
	if(p_last_comment_id > 0) {
		last_comment_id = p_last_comment_id;
	}
}
//------------------------------------------------------------------------------
var button_type;
function SendFormAddComment(form, p_is_blog) {
	// Clear error message because new action
	var err_div			= xGetElementById('error_message');
	err_div.style.display = 'none';
	err_div.innerHTML	= '';

	form.action = 'http://'+document.location.hostname+'/p/add_comment.cgi';

//	return true;  //  uncomment this to disable ajax
	
	var form_topic_id	= form.topic_id.value;
	var form_r			= form.r.value;
	var form_message	= form.message.value;

	var form_login		= (form.login ? form.login.value : '');
	var form_password	= (form.password ? form.password.value : '');
	var form_authorize	= (form.authorize ? form.authorize.value : '');
	var form_subscribe	= (form.subscribe.checked ? form.subscribe.value : '');;
	var form_favourite	= (form.favourite.checked ? form.favourite.value : '');;
	var form_avatar		= (form.avatar ? form.avatar.value : '');
	var form_add		= form.add.value;
	var form_preview	= form.preview.value;
	var form_user_type	= '';
	if(form.user_type.length != undefined) {
		if(form.user_type[0].checked) {
			form_user_type	= form.user_type[0].value;
		}else if(form.user_type[1].checked) {
			form_user_type	= form.user_type[1].value;
		}
	}else {
		form_user_type = form.user_type.value;
	}

	// User enter login and password. So we don't use ajax	
	if(form_user_type == 'notanon' && form.authorize.checked) {
		return true;
	}

	var data =	'ajax=1&'+
				'topic_id='+form_topic_id+'&'+
				'r='+form_r+'&'+
				'message='+my_encodeURIComponent(form_message)+'&'+
				'user_type='+form_user_type+'&'+
				'login='+my_encodeURIComponent(form_login)+'&'+
				'password='+my_encodeURIComponent(form_password)+'&'+
				'authorize='+form_authorize+'&'+
				'subscribe='+form_subscribe+'&'+
				'avatar='+form_avatar+'&'+
				'button='+button_type+'&'+
				'favourite='+form_favourite;

	var ajax = new Ajax();
	var url = 'http://'+document.location.hostname+'/p/add_comment.cgi';

	if(!ajax.init()) {
		return true;
	}

	is_runned = 3;

	if(button_type == 'add') {
		data += '&add=' + form_add;
		ajax.doPost(url, data, GetNewComments, func_err);
	}else if(button_type == 'preview') {
		data += '&preview=' + form_preview;
		ajax.doPost(url, data, UpdateComments, func_err);
	}else {
		return true;
	}

	return false;
}
//------------------------------------------------------------------------------
function SetAddButton() {
	button_type = 'add';
	var form_elem = xGetElementById('comment_form');
	form_elem.button.value = button_type;
}
//------------------------------------------------------------------------------
function SetPreviewButton() {
	button_type = 'preview';
	var form_elem = xGetElementById('comment_form');
	form_elem.button.value = button_type;
}
//------------------------------------------------------------------------------
function my_encodeURIComponent(s){
	if(typeof encodeURIComponent=="function"){
		return encodeURIComponent(s);
	}else{
		return escape(s).replace(new RegExp('\\+','g'), '%2B');
	}
}
//------------------------------------------------------------------------------
function ShowPreview( p_message ) {
	var err_div			= xGetElementById('error_message');
	err_div.innerHTML	= p_message;
	err_div.style.display = 'block';
	
	resize_images_timeout = setTimeout(fix_comment, 1000);

	is_runned = undefined;
	runChecker();
	return;
}
//------------------------------------------------------------------------------
function fix_comment() {
	resize_images();
	favicons_processor();
	fix_all_png_images();
}
//------------------------------------------------------------------------------
function HideFormIfCan() {
	var form_elem = xGetElementById('comment_form');
	if(!form_elem) {
		return;
	}
	if(form_elem.message.value == '') {
		var new_span = document.createElement( "SPAN" );
		xParent(form_elem, true).insertBefore( new_span, form_elem );
		new_span.innerHTML = 'Превышено максимальное количество комментариев к одной записи.';
		form_elem.style.display = 'none';
	}
}
//------------------------------------------------------------------------------
function GetToolbar(context, is_not_guest) {
	var toolbar = '<table cellpadding=0 cellspacing=0 width="100%"><tr><td colspan=3 width="100%"><table height="19px" cellpadding=0 cellspacing=0 class="hbutton_bar"><td class="toolbar"><a href="javascript:addTag(\'' + 
		context + '\',\'B\');" class="hbutton"><img src=http://beon.ru/i/smiles/b.gif width=12 height=15 border=0 alt="B" title="Жирный"></a><a href="javascript:addTag(\'' + 
		context + '\',\'I\');" class="hbutton"><img src=http://beon.ru/i/smiles/i.gif width=10 height=15 border=0 alt="I" title="Курсив"></a><a href="javascript:addTag(\'' +
		context + '\',\'U\');" class="hbutton"><img src=http://beon.ru/i/smiles/u.gif width=12 height=15 border=0 alt="U" title="Подчёркнутый"></a><a href="javascript:addTag(\'' +
		context + '\',\'S\');" class="hbutton"><img src=http://beon.ru/i/smiles/s.gif width=16 height=15 border=0 alt="S" title="Зачеркнутый"></a><a href="javascript:addTag(\'' +
		context + '\',\'H\');" class="hbutton"><img src=http://beon.ru/i/smiles/h.gif width=12 height=15 border=0 alt="H" title="Подзаголовок"></a><a href="javascript:addTag(\'' +
		context + '\',\'OFF\');" class="hbutton"><img src=http://beon.ru/i/smiles/off.gif width=31 height=15 border=0 alt="OFF" title="Офф-топик, не по теме"></a>&nbsp;<a href="javascript:addTag(\'' +
		context + '\',\'CENTER\');" class="hbutton"><img src=http://beon.ru/i/smiles/center.gif width=17 height=15 border=0 alt="CENTER" title="По центру"></a><a href="javascript:addTag(\'' +
		context + '\',\'RIGHT\');" class="hbutton"><img src=http://beon.ru/i/smiles/right.gif width=17 height=15 border=0 alt="RIGHT" title="По правому краю"></a>&nbsp;';

	if (context == 'topic_form') {
		toolbar += '<a href="javascript:addTag(\'' + context + '\',\'SPOILER\');" class="hbutton"><img src=http://beon.ru/i/smiles/spoiler.gif width=22 height=15 border=0 alt="SPOILER" title="Скрыть текст за ссылкой &quot;Подробнее&hellip;\&quot;"></a>&nbsp;';
	}
	
	if (context == 'comment_form' && (window.getSelection || document.getSelection || document.selection)) {
			toolbar += '<a href="javascript:false;" onclick="return false;" class="hbutton"><img src=http://beon.ru/i/smiles/quote.gif width=46 height=15 border=0 alt="Quote" title="Вставить цитату" onMouseOver="get_quote();" onMouseDown="quote(\'' + context + '\');"></a>&nbsp;';
	}

	toolbar += '<a href="javascript:clearFormating(\'' + context + '\');" class="hbutton"><img src=http://beon.ru/i/smiles/clear.png width=15 height=15 border=0 alt="Clear formating" title="Удалить форматирование"></a>&nbsp;';

	if(is_not_guest) {
		toolbar += '<a href="javascript:addImage(\'' + context + '\');" class="hbutton"><img src=http://beon.ru/i/smiles/image.gif width=19 height=15 border=0 alt="Image" title="Вставить изображение"></a>';
	}
	toolbar += '<a href="javascript:addVideo(\'' + context + '\');" class="hbutton"><img src=http://beon.ru/i/video.gif width=15 height=15 border=0 alt="Video" title="Вставить видео-ролик"></a>';

//	if(is_not_guest) {
		toolbar += '<a href="javascript:addUser(\'' + context + '\');" class="hbutton"><img src=http://beon.ru/i/smiles/user.png width=15 height=15 border=0 alt="User" title="Вставить ссылку на пользователя"></a><a href="javascript:addBlog(\'' + context + '\');" class="hbutton"><img src=http://beon.ru/i/smiles/blogs.png width=22 height=15 border=0 alt="Blog" title="Вставить ссылку на дневник"></a>';
//	}

	toolbar += '&nbsp;<a href="javascript:translit(\'' + 
		context + '\');" class="hbutton"><img src=http://beon.ru/i/smiles/translit.gif width=54 height=15 border=0 alt="Translit" title="Перекодировать выделенный текст из латиницы в кириллицу"></a><a href=http://beon.ru/i/translit.html target=_blank onClick="window.open(this.href,\'\',\'toolbar=0,scrollbars=0,location=0,statusbar=1,menubar=0,resizable=1,width=760,height=160\');return false;" style="vertical-align:4px;margin-left:2px;margin-bottom:-1px;" title="Таблица транслитерации">?</a>&nbsp;</td></table>' +
		'</td></tr><tr><td colspan=3><table border=0 cellpadding=0 cellspacing=0 width=100%><tr><td rowspan=2 class="smiles_box">' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-) \');"><img src=http://beon.ru/i/smiles/smile.png alt=":-)" title=":-) улыбка" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-( \');"><img src=http://beon.ru/i/smiles/sad.png alt=":-(" title=":-( разочарование" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\';-) \');"><img src=http://beon.ru/i/smiles/wink.png alt=";-)" title=";-) подмигивание" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-* \');"><img src=http://beon.ru/i/smiles/kiss.png alt=":-*" title=":-* поцелуйчик" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-D \');"><img src=http://beon.ru/i/smiles/big-smile.png alt=":-D" title=":-D смеяться" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-O \');"><img src=http://beon.ru/i/smiles/surprised.png alt=":-O" title=":-O удивление" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-P \');"><img src=http://beon.ru/i/smiles/tongue-sticking-out.png alt=":-P" title=":-P показывать язык" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'X-( \');"><img src=http://beon.ru/i/smiles/angry.png alt="X-(" title="X-( злость" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\']:-) \');"><img src=http://beon.ru/i/smiles/devil.png alt="]:-)" title="]:-) чёртик" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'O:-) \');"><img src=http://beon.ru/i/smiles/angel.png alt="O:-)" title="O:-) ангелочек" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':\\\'( \');"><img src=http://beon.ru/i/smiles/cry.png alt=":\'(" title=":\'( плакать" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-[ \');"><img src=http://beon.ru/i/smiles/upset.png alt=":-[" title=":-[ огорчение" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-\\\\ \');"><img src=http://beon.ru/i/smiles/confused.png alt=":-\\" title=":-\\ смущение" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-| \');"><img src=http://beon.ru/i/smiles/undecided.png alt=":-|" title=":-| неуверенность" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-? \');"><img src=http://beon.ru/i/smiles/thinking.png alt=":-?" title=":-? хм-м-м..." border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\';~) \');"><img src=http://beon.ru/i/smiles/cunning.png alt=";~)" title=";~) хитрая улыбка" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'(:| \');"><img src=http://beon.ru/i/smiles/tired.png alt="(:|" title="(:| усталость" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'8-} \');"><img src=http://beon.ru/i/smiles/crazy.png alt="8-}" title="8-} сумасшествие" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':-$ \');"><img src=http://beon.ru/i/smiles/shhh.png alt=":-$" title=":-$ тц-ц-ц!" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'8-| \');"><img src=http://beon.ru/i/smiles/shocked.png alt="8-|" title="8-| я в шоке!" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'B-) \');"><img src=http://beon.ru/i/smiles/sun-glasses.png alt="B-)" title="B-) в очках!" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\':^) \');"><img src=http://beon.ru/i/smiles/turn-red.png alt=":^)" title=":^) покраснеть!" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'=^B \');"><img src=http://beon.ru/i/smiles/thumbs-up.png alt="=^B" title="=^B классно!" border=0 height=15 width=15></a>&nbsp;' +
		'<a href="javascript:addSmile(\'' + context + '\',\'=,B \');"><img src=http://beon.ru/i/smiles/thumbs-down.png alt="=,B" title="=,B отстой" border=0 height=15 width=15></a>' +
		'</td><td align=right><a href="javascript:Resize(\'' + context + '\', -300);"><img border=0 src=http://beon.ru/i/decrise.gif alt="уменьшить" title="уменьшить"></a></td></tr><tr><td align=right><a href="javascript:Resize(\'' + context + '\', 300);"><img border=0 src=http://beon.ru/i/incrise.gif alt="увеличить" title="увеличить"></a></td></tr></table></td></tr></table>';

	return toolbar;
}
//------------------------------------------------------------------------------





}
/*
     FILE ARCHIVED ON 04:59:04 Jul 05, 2007 AND RETRIEVED FROM THE
     INTERNET ARCHIVE ON 08:34:39 Aug 23, 2022.
     JAVASCRIPT APPENDED BY WAYBACK MACHINE, COPYRIGHT INTERNET ARCHIVE.

     ALL OTHER CONTENT MAY ALSO BE PROTECTED BY COPYRIGHT (17 U.S.C.
     SECTION 108(a)(3)).
*/
/*
playback timings (ms):
  captures_list: 185.659
  exclusion.robots: 0.268
  exclusion.robots.policy: 0.259
  RedisCDXSource: 0.728
  esindex: 0.013
  LoadShardBlock: 167.964 (3)
  PetaboxLoader3.datanode: 182.206 (4)
  CDXLines.iter: 13.993 (3)
  load_resource: 55.414
  PetaboxLoader3.resolve: 21.496
*/