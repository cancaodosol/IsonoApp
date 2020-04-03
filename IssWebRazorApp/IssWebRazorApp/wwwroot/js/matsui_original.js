function displayNextElement(obj) {
	console.log(obj);
	var target = obj.nextElementSibling;
	console.log(target);
	var check = obj.nextElementSibling.style.display;
	console.log(check);
	if (check === "block") {
		disapper(target);
	} else {
		apper(target);
	}
}

function apper(element){
	element.style.display = "block";
}

function disapper(elementj){
	element.style.display = "none";
}

function showReadMore(btn)
{
	var targetId = btn.getAttribute("href");
	$(targetId).hide();
	//$(targetId).slideDown("slow");
	return false;
}