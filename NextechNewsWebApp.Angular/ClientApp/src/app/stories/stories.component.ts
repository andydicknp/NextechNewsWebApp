import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Story } from './story';

@Component({
    selector: 'app-stories',
    templateUrl: './stories.component.html',
    styleUrls: ['./stories.component.css']
})
/** stories component*/
export class StoriesComponent {
  public columns: string[] = ['id', 'title', 'url'];
  public stories: Story[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string) {
  }

  ngOnInit() {
    this.http.get<Story[]>(this._baseUrl + 'api/Story')
      .subscribe(result => {
        this.stories = result;
      }, error => console.error(error));
  }
}
