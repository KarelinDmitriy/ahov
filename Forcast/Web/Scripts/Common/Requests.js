
function RenderGet (url, target) {
	$.ajax(
	{
		url,
		cache: false,
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
		cache: false,
		type: "POST",
		data: data,
		success:
			function (dataFrom) {
				$(target).after(dataFrom);
			}
	});
}

function SendAndRemove(url, target, targetOnFail) {
	$.ajax(
		{
			url,
			cache: false,
			dataType: "json",
			success:
				function(data) {
					alert(data.success);
					if (data.success) {
						$(target).remove();
					} else {
						alert("Все плохо");
					}
				},
			error: 
				function(r, text) {
					alert(text);
				}
		}
	);
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

