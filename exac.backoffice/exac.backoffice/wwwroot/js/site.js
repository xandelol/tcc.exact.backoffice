/**
 * Created by guidpt on 9/30/17.
 */


    

jQuery(document).ready(function($) {
    
    $("#logout").click(function () {
        show_loading_bar(70); // Fill progress bar to 70% (just a given value)
        $.ajax({
            url: "../Account/logout",
            method: 'POST',
            dataType: 'json',
            success: function(resp) {
                show_loading_bar({
                    delay: .5,
                    pct: 100,
                    finish: function() {

                        // Redirect after successful login page (when progress bar reaches 100%)
                        if (resp.ok) {
                            window.location.href = '/';
                        }
                    }
                });
            }
        });
    });
});