

$(document).ready(function () {
   
    $("#MenuButton").click(function () {

        $("#MenuContent").toggle();
    });

    $("#ChangeImageButton").click(function () {
        var file = document.getElementById("ImageUploaded").files[0];

        if (file != null) {
            if (file.size < (1024 * 1024 * 4)) {

                $("#ChangeImage").submit();

            }
            else {
                document.getElementById("lbl").innerHTML = "Image is Large , must be less than 4MB";

            }
        }
    });
    $("#ChangeCoverButton").click(function () {
        var file = document.getElementById("CoverUploaded").files[0];

        if (file != null) {
            if (file.size < (1024 * 1024 * 4)) {

                $("#ChangeCover").submit();

            }
            else {
                document.getElementById("lblCover").innerHTML = "Image is Large , must be less than 4MB";

            }
        }
    });

    
});

function ShowEditModel(caption, id, type) {
    if (type == 'Text')
    {
        $("#EditCaption").attr('required','required');
    }
    $("#EditPostType").val(type);
        $("#EditCaption").val(caption);
        $("#EditPostID").val(id);
        $("#EditModal").modal('show');
    }

function ShowDeleteModal(id) {
    $("#DeletePostID").val(id);
    $("#DeleteModal").modal('show');
}
var postID;
function AddComment(commenter, image)
{

    var comment = $("#CommentText").val();
   
    if (comment.length > 0)
    {
        $("#CommentText").val('');
        $("#CommentsList").append("<li id='CommentItem' style='display=inline-flex;padding:10px'>" + "<img width='50' height='30' style='border-radius:100%' src='" + image + "'/>" + "<text style='margin-top:3px;position:absolute;color:black'>" + commenter + "</text>" + "<br/>" + "<p style='font-weight:lighter;'> " + comment + "</p>" + "</li>");
        var formData = new FormData();
        formData.append('postID', postID);
        formData.append('comment', comment);
       
        $.ajax(
            {
            type: "POST",
            url: '/MyProfile/AddComment',
            contentType: false,
            processData: false,
            data: formData,
            success: function (result)
            {
                alert(result);
            }
            , error: function ()
            {
                alert("Unknown Error");
            }

            }
            );
    }
  
}


function Like(id,liker,poster,likes) {
    var formData = new FormData();
    formData.append('postID',id);
    formData.append('liker', liker);
    formData.append('poster', poster);
  
    $.ajax({
        type: "POST",
        url: '/MyProfile/Like',
        contentType: false,
        processData: false,
        data: formData,
        success: function (result) {
            if (result == "IsLiked")
            {
                var liken = parseInt($("#" + id + "").find('#LikeNumber').html())-1;
                $("#" + id).find('#LikeNumber').html(liken);
                $("#" + id).find('#Like').css('color', 'black');
                $("#" + id).find("#LikeIcon").css('opacity', '1');
               
             

            }
            else
            {
                var liken = parseInt($("#" + id + "").find('#LikeNumber').html()) + 1;
                $("#" + id).find('#LikeNumber').html(liken);
                $("#" + id).find('#Like').css('color', '#c3c3c3');
                $("#" + id).find("#LikeIcon").css('opacity', '0.2');
              
               
            }
        },

        error: function () {

            alert("Unknown Error");
        }

    });
}
function AddComment(postID) {
   
    var comment = document.getElementById('CommentText').value;
   
    if (comment.length > 0)
    {
        var formData = new FormData();
        formData.append('postID', postID);
      
        formData.append('comment', comment);
      
        $.ajax({
            type: "POST",
            url: '/MyProfile/AddComment',
            contentType: false,
            processData: false,
            data: formData,
            success: function (result) {
                location.reload();
            },

            error: function () {
                alert('Unknown Error')
             
            }

        });
    }
   

}

function Back()
{
    var urlParams = new URLSearchParams(window.location.search);
    if (urlParams.get('posterID')==null) {

        window.location.href = '/MyProfile/MyProfile';
    }
   
    else
    {
        window.location.href = '/User/User?id=' + urlParams.get('posterID');
    }
}
function Follow(followed, follower)
{
    if ($("#Follow").html() == 'Follow')
    {
        var formData = new FormData();
        formData.append('followed', followed);

        formData.append('follower', follower);

        $.ajax({
            type: "POST",
            url: '/User/Follow',
            contentType: false,
            processData: false,
            data: formData,
            success: function (result)
            {

                $("#Follow").removeClass("btn btn-primary");
                $("#Follow").addClass("btn btn-secondary");
                $("#Follow").html('Unfollow');
                var num = parseInt($("#FollowersNumber").html()) + 1;
                $("#FollowersNumber").html( num+ ' Followers');
            },

            error: function ()
            {
                alert('Unknown Error')
            }

        });
    }
    else if ($("#Follow").html() == 'Unfollow')
    {
        var formData = new FormData();
        formData.append('followed', followed);

        formData.append('follower', follower);

        $.ajax({
            type: "POST",
            url: '/User/Unfollow',
            contentType: false,
            processData: false,
            data: formData,
            success: function (result) {
                $("#Follow").removeClass("btn btn-secondary");
                $("#Follow").addClass("btn btn-primary");
                $("#Follow").html('Follow');
                var num = parseInt($("#FollowersNumber").html()) - 1;
                $("#FollowersNumber").html(num + ' Followers');
            },

            error: function () {
                alert('Unknown Error')

            }

        });
    }
  
}

