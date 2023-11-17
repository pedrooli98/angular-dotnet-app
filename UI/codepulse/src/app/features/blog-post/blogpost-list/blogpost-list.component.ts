import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { BlogPostsService } from '../services/blogposts-services.service';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit{
  blogPosts$?: Observable<BlogPost[]>

  constructor(private service: BlogPostsService){}
  
  ngOnInit(): void {
    this.blogPosts$ = this.service.getAllBlogPosts();
  }

}
