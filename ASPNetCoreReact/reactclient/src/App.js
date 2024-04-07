import React, { useState, useEffect } from "react";
import Constants from "./utilities/constants";
import PostCreateForm from "./components/PostCreateForm";
import PostUpdateForm from "./components/PostUpdateForm";
import { NavLink } from "react-router-dom";

export default function App() {
  const [posts, setPosts] = useState([]);
  const [showingCreateNewPostForm, setShowingCreateNewPostForm] = useState(false);
  const [postCurrentlyBeingUpdated, setPostCurrentlyBeingUpdated] = useState(null);

  useEffect(() => {
    getPosts();
  }, []);

  function getPosts() {
    const url = Constants.API_URL_GET_ALL_POSTS;

    fetch(url, {
      method: 'GET'
    })
      .then(response => response.json())
      .then(postsFromServer => {
        setPosts(postsFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  function deletePost(postId) {
    const url = `${Constants.API_URL_DELETE_POST_BY_ID}/${postId}`;

    fetch(url, {
      method: 'DELETE'
    })
      .then(response => response.json())
      .then(responseFromServer => {
        console.log(responseFromServer);
        onPostDeleted(postId);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  return (
    
    <div className="container">
      <div>
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <div className="container-fluid">
          <a className="navbar-brand" href="#">React CRUD App</a>
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav">
              <li className="nav-item">
                <NavLink to="#" className="nav-link" onClick={getPosts}>Get Posts</NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="#" className="nav-link" onClick={() => setShowingCreateNewPostForm(true)}>Create Post</NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>  
      </div>
      <div className="row">
      <div className="container mt-4">
        {/* Show the update form as the main content if a post is selected for update */}
        {postCurrentlyBeingUpdated && (
          <PostUpdateForm post={postCurrentlyBeingUpdated} onPostUpdated={onPostUpdated} />
        )}

        {/* Otherwise, show the rest of the UI */}
        {!postCurrentlyBeingUpdated && (
          <>
            {showingCreateNewPostForm && <PostCreateForm onPostCreated={onPostCreated} />}
            
            {/* Display posts in a more focused or highlighted manner */}
            <div className="row">
              {posts.map(post => (
                <div key={post.postId} className="col-md-4 mb-4">
                  <div className="card position-relative">
                    <div className="card-body">
                      <h5 className="card-title">{post.title}</h5>
                      <p className="card-text">{post.content}</p>
                      <div className="position-absolute top-0 end-0 p-2">
                      <button onClick={() => setPostCurrentlyBeingUpdated(post)} className="btn btn-outline-primary">
                        <i className="bi bi-pencil-square"></i>
                      </button>
                      <button onClick={() => {if(window.confirm(`Are you sure you want to delete the post titled "${post.title}"?`)) deletePost(post.postId) }} className="btn btn-outline-danger ms-2">
                        <i className="bi bi-x-lg"></i>
                      </button>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </>
        )}
      </div>
</div>
    </div>
  );

  function renderPostsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">PostId</th>
              <th scope="col">Title</th>
              <th scope="col">Content</th>
              <th scope="col">Operations</th>
            </tr>
          </thead>
          <tbody>
            {posts.map((post) => (
              <tr key={post.postId}>
                <th scope="row">{post.postId}</th>
                <td>{post.title}</td>
                <td>{post.content}</td>
                <td>
                  <button onClick={() => setPostCurrentlyBeingUpdated(post)} className="btn btn-dark btn-lg mx-3 my-3">Update</button>
                  <button onClick={() => { if(window.confirm(`Are you sure you want to delete the post titled "${post.title}"?`)) deletePost(post.postId) }} className="btn btn-secondary btn-lg">Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <button onClick={() => setPosts([])} className="btn btn-dark btn-lg w-100">Empty React posts array</button>
      </div>
    );
  }

  function onPostCreated(createdPost) {
    setShowingCreateNewPostForm(false);

    if (createdPost === null) {
      return;
    }

    alert(`Post successfully created. After clicking OK, your new post tilted "${createdPost.title}" will show up in the table below.`);

    getPosts();
  }

  function onPostUpdated(updatedPost) {
    setPostCurrentlyBeingUpdated(null);

    if (updatedPost === null) {
      return;
    }

    let postsCopy = [...posts];

    const index = postsCopy.findIndex((postsCopyPost, currentIndex) => {
      if (postsCopyPost.postId === updatedPost.postId) {
        return true;
      }
    });

    if (index !== -1) {
      postsCopy[index] = updatedPost;
    }

    setPosts(postsCopy);

    alert(`Post successfully updated. After clicking OK, look for the post with the title "${updatedPost.title}" in the table below to see the updates.`);
  }

  function onPostDeleted(deletedPostPostId) {
    let postsCopy = [...posts];

    const index = postsCopy.findIndex((postsCopyPost, currentIndex) => {
      if (postsCopyPost.postId === deletedPostPostId) {
        return true;
      }
    });

    if (index !== -1) {
      postsCopy.splice(index, 1);
    }

    setPosts(postsCopy);

    alert('Post successfully deleted. After clicking OK, look at the table below to see your post disappear.');
  }
}
