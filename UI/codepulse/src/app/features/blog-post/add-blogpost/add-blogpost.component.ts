import { Component, OnDestroy } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.models';
import { Subscription } from 'rxjs';
import { BlogPostsService } from '../services/blogposts-services.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnDestroy{
  model:AddBlogPost
  private addBlogPostSubscription?: Subscription

  constructor(private service: BlogPostsService, private router: Router) {
    this.model = {
      title : '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date()
    }
  }
  
  onFormSubmit(): void {
    this.addBlogPostSubscription = this.service.addBlogPost(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts')
        }
      })
  }

  ngOnDestroy(): void {
    this.addBlogPostSubscription?.unsubscribe();
  }
}
