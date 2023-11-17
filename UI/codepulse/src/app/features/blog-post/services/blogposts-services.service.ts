import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AddBlogPost } from '../models/add-blog-post.models';

@Injectable({
  providedIn: 'root'
})
export class BlogPostsService {

  constructor(private http: HttpClient) { }

  addBlogPost(model: AddBlogPost): Observable<void> {
   return this.http.post<void>(`${environment.apiBaseUrl}/api/BlogPosts`, model)
  }
}