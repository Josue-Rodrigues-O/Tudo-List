import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class UserImageService {
  private readonly baseUrl: string = 'api/user-images';

  constructor(private readonly httpClient: HttpClient, private readonly authService: AuthService) { }

  public GetByUserId(userId: number) {
    const url = `${this.baseUrl}/get-by-user-id-async/${userId}`;
    return this.httpClient.get(url, { responseType: 'blob' });
  }

  public Upload(img: File) {
    const user = this.authService.getCurrentUser();
    const url = `${this.baseUrl}/upload-async/${user()?.id}`;
    const formData = new FormData();
    formData.append('file', img);
    return this.httpClient.post(url, formData, { responseType: 'blob' });
  }

  private abrirDB(): Promise<IDBDatabase> {
    return new Promise((resolve, reject) => {
      const request = indexedDB.open('imgProfileDb', 1);

      request.onupgradeneeded = (event: IDBVersionChangeEvent) => {
        const db = (event.target as IDBOpenDBRequest).result;

        if (!db.objectStoreNames.contains('imgProfile')) {
          db.createObjectStore('imgProfile');
        }
      };

      request.onsuccess = () => resolve(request.result);
      request.onerror = () => reject(request.error);
    });
  }

  async saveImgLocally(blob: Blob): Promise<void> {
    if (!(blob instanceof Blob) || blob.size === 0) {
      throw new Error('Blob inválido');
    }

    const db = await this.abrirDB();
    const tx = db.transaction('imgProfile', 'readwrite');
    tx.objectStore('imgProfile').put(blob, 'img');

    return new Promise((resolve, reject) => {
      tx.oncomplete = () => resolve();
      tx.onerror = () => reject(tx.error);
      tx.onabort = () => reject(tx.error);
    });
  }

  async getLocalImg(): Promise<string | null> {
    const db = await this.abrirDB();
    const tx = db.transaction('imgProfile', 'readonly');
    const req = tx.objectStore('imgProfile').get('img');

    return new Promise((resolve, reject) => {
      req.onsuccess = () => {
        const blob = req.result;

        if (blob) {
          resolve(URL.createObjectURL(blob));
        } else {
          resolve(null);
        }
      };

      req.onerror = () => reject(req.error);
    });
  }
}
