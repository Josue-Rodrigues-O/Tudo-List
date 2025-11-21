import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserImageService {
  private readonly baseUrl: string = 'api/UserImages';

  constructor(private readonly httpClient: HttpClient) { }

  public GetByUserId(userId: number) {
    const url = `${this.baseUrl}/get-by-user-id-async/${userId}`;
    return this.httpClient.get<Blob>(url);
  }

  public Upload(userId: number, img: File) {
    const url = `${this.baseUrl}/upload-async/${userId}`;
    const formData = new FormData();
    formData.append('file', img);
    return this.httpClient.post<Blob>(url, img);
  }
}
