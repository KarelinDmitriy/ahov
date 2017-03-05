
function RenderGet(url, target, waitId) {
	var w = $(waitId);
	w.removeClass("hidden");
	$.ajax(
	{
		url,
		cache: false,
		success:
			function (data) {
				$(target).after(data);
				w.addClass("hidden");
			},
		error:
			function (r, text) {
				w.addClass("hidden");
			}
	});
}

function RenderPost(url, data, target, waitId) {
	var w = $(waitId);
	w.removeClass("hidden");
	$.ajax(
	{
		url: url,
		cache: false,
		type: "POST",
		data: data,
		success:
			function (dataFrom) {
				$(target).after(dataFrom);
				w.addClass("hidden");
			},
		error:
			function(r, text) {
				w.addClass("hidden");
			}
	});
}

function SendAndRemove(url, target, waitId) {
	var w = $(waitId);
	w.removeClass("hidden");
	$.ajax(
		{
			url,
			cache: false,
			dataType: "json",
			success:
				function (data) {
					alert(data.success);
					if (data.success) {
						$(target).remove();
					} else {
						alert("Все плохо");
					}
					w.addClass("hidden");
				},
			error:
				function (r, text) {
					w.addClass("hidden");
					alert(text);
				}
		}
	);
}

function SubmitAndRender(form, url, target, waitId) {
	var w = $(waitId);
	w.removeClass("hidden");
	$.ajax(
	{
		url: url,
		type: "POST",
		data: $(form).serialize(),
		success:
			function (dataFrom) {
				$(target).after(dataFrom);
				w.addClass("hidden");
			},
		error:
			function (r, text) {
				w.addClass("hidden");
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

