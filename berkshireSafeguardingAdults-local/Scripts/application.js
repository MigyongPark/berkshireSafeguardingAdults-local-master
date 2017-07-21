	
	$(document).ready(function() {
		
		$(".item:nth-child(odd), ul li:nth-child(odd), dl dt:nth-child(odd), dl dd:nth-child(odd), tr:nth-child(odd)").addClass("odd");
		$(".item:nth-child(even), ul li:nth-child(even), dl dt:nth-child(even), dl dd:nth-child(even), tr:nth-child(even)").addClass("even");

		// DROP DOWN - CLICK & HOVER - MAIN NAV
		$(".navigation nav.main ul li").hover(function(){ $(this).toggleClass("hover"); });
		$(".navigation nav.main ul li > i").click(function(){
			if ($(".navigation nav.main ul li > i").length ) { 
				$(this).parent().toggleClass("open");
				$(this).parent().siblings().removeClass("open");
			}
			else{
				$(this).parent().toggleClass("open");
			}
		});
		 $("html").click(function() {
			$(".navigation nav.main ul li.open").removeClass("open");
		 });
		$(".navigation nav.main ul li > i").click(function(e){
			e.stopPropagation(); 
		});
		
		// EXPAND MOBILE NAVIVAGTION  
		$(".navigation a.expand").click(function(){
			if ($(".navigation .reveal").length ) { 
				$(".navigation a.expand").toggleClass('active');
				$(".navigation a.expand em").text('Close');
				$("html").toggleClass('reveal_out');
			}
			else{
				$(".navigation a.expand").toggleClass('active');
				$(".navigation a.expand em").text('Menu');
				$("html").toggleClass('reveal_out');
			}
		});

		// SLIDESHOWS
      	$(".banner .slides").slick({
			dots: false,
			infinite: true,
  			adaptiveHeight: true,
			speed: 700,
			fade: true,
			easing: 'linear',
			prevArrow: '<div class="slick-prev"><i class="fa fa-angle-left fa-3x"></i>',
			nextArrow: '<div class="slick-next"><i class="fa fa-angle-right fa-3x"></i>'
		});
      	$(".slideshow").slick({
			dots: false,
			infinite: true,
  			adaptiveHeight: true,
			speed: 1000,
			fade: true,
			easing: 'linear',
			prevArrow: '<div class="slick-prev"><i class="fa fa-angle-left fa-2x"></i>',
			nextArrow: '<div class="slick-next"><i class="fa fa-angle-right fa-2x"></i>'
		});
      	$(".image_carousel .slides").slick({
			dots: true,
			infinite: true,
			speed: 200,
			easing: 'linear',
			prevArrow: '<div class="slick-prev"><i class="fa fa-angle-left fa-2x"></i>',
			nextArrow: '<div class="slick-next"><i class="fa fa-angle-right fa-2x"></i>',
			arrows: false,
			slidesToShow: 4,
			slidesToScroll: 4,
				responsive: [
				{
					breakpoint: 1200,
					settings: {
						slidesToShow: 3,
						slidesToScroll: 3,
					}
				},
				{
					breakpoint: 768,
					settings: {
						slidesToShow: 2,
						slidesToScroll: 2
					}
				},
				{
					breakpoint: 480,
					settings: {
						slidesToShow: 1,
						slidesToScroll: 1,
						dots: false,
						arrows: true
					}
				}
			]
		});
		
		// BACK TO TOP
		if ( ($(window).height() + 100) < $(document).height() ) {
	        $('#top-link-block').addClass('show').affix({
	            // how far to scroll down before link "slides" into view
	            offset: {top:100}
	        });
		}
				
	});
	
	// OPEN MULTIPLE COLLAPSE PANELS
	// http://www.bootply.com/90JfjI2Q7n
	$(function () {	
				
		$('a[data-toggle="collapse"]').on('click',function(){
			var objectID=$(this).attr('href');
			if($(objectID).hasClass('in'))
			{
				$(objectID).collapse('hide');
			}
			else{
				$(objectID).collapse('show');
			}
		});
		
		// EXPAND ALL
		$('#expandAll').on('click',function(){
			$('a[data-toggle="collapse"]').each(function(){
				var objectID=$(this).attr('href');
				if($(objectID).hasClass('in')===false)
				{
					$(objectID).collapse('show');
				}
			});
		});
		
		// COLLAPSE ALL
		$('#collapseAll').on('click',function(){
			$('a[data-toggle="collapse"]').each(function(){
				var objectID=$(this).attr('href');
				$(objectID).collapse('hide');
			});
		});
	});
	
	// TRIGGER ANIMATIONS
	// http://www.oxygenna.com/tutorials/scroll-animations-using-waypoints-js-animate-css 
	$(window).bind('load', function(){
		
		function onScrollInit( items, trigger ) {
		
			items.each( function() {
				var osElement = $(this),
				osAnimationClass = osElement.attr('data-os-animation'),
				osAnimationDelay = osElement.attr('data-os-animation-delay');

				osElement.css({
					'-webkit-animation-delay':  osAnimationDelay,
					'-moz-animation-delay':     osAnimationDelay,
					'-ms-animation-delay':     osAnimationDelay,
					'animation-delay':          osAnimationDelay
				});

				var osTrigger = ( trigger ) ? trigger : osElement;

				osTrigger.waypoint(function() {
					osElement.addClass('animated').addClass(osAnimationClass);
				},{
					triggerOnce: true,
					offset: '85%'
				});
			});
		
		}
		onScrollInit( $('.os-animation') );
	
	});
	
	// LIGTHBOX
	$(document).delegate('*[data-toggle="lightbox"]', 'click', function(event) {
    	event.preventDefault();
        $(this).ekkoLightbox();
	}); 
	
	$(window).on("load resize scroll",function(e){
		
		var img1 = $(".gallery img");
		$(".gallery .overlayicon").css({width:img1.width(), height:img1.height()});
		
		var img2 = $(".promo_pods .videopod img");
		$(".promo_pods .videopod .overlayicon").css({width:img2.width(), height:img2.height()});
		
		var img3 = $(".promo_pods .imagepod img");
		$(".promo_pods .imagepod .overlayicon").css({width:img3.width(), height:img3.height()});
		
		var img3 = $(".video-component .image.video img");
		$(".video-component .image.video .overlayicon").css({width:img3.width(), height:img3.height()});
		
		var img3 = $(".text-with-image_video .image.video img");
		$(".text-with-image_video .image.video .overlayicon").css({width:img3.width(), height:img3.height()});
		
	});

