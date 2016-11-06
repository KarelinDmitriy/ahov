
function RenderGet (url, target) {
	$.ajax(
	{
		url,
		success:
			function(data) {
				$(target).after(data);
			}
	});
}

function RenderPost(url, data, target) {
	$.ajax(
	{
		url: url,
		type: "POST",
		data: data,
		success:
			function (dataFrom) {
				$(target).after(dataFrom);
			}
	});
}

function SubmitAndRender(form, url, target) {
	$.ajax(
	{
		url: url,
		type: "POST",
		data: $(form).serialize(),
		success:
			function (dataFrom) {
				$(target).after(dataFrom);
			}
	});
	//$(form).submit(function() { // catch the form's submit event
	//	$.ajax(
	//		{
	//			async: false,
	//			data: $(form).serialize(), // get the form data
	//			type: "POST", // GET or POST
	//			url: url, // the file to call
	//			success: function(dataFrom) {
	//				$(target).before(dataFrom);
	//			},
	//			error: function(er) {
	//				alert(er);
	//			}
	//		});
	//});
	//return false;
}

