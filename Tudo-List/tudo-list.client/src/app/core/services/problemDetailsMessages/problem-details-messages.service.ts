import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProblemDetailsMessagesService {
  constructor() {}

  getMessages(error: any) {
    let messages: Array<string> = new Array<string>();
    let title: string = 'Error';
    if (error.hasOwnProperty('errors')) {
      error.errors = error.errors || [];
      Object.keys(error.errors).forEach((errMessages: any) => {
        messages.push(error.errors[errMessages]);
      });
      title = error.title;
    } else {
      messages.push(error.title);
    }

    return {
      title: title,
      messages: messages,
    };
  }
}
