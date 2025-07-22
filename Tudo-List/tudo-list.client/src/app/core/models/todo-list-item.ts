import { PriorityEnum } from '../enums/priority-enum';
import { StatusEnum } from '../enums/status-enum';

export interface TodoListItem {
  id?: string;
  itemId?: string;
  title: string;
  description: string;
  status: StatusEnum;
  priority: PriorityEnum;
  creationDate?: Date;
  userId?: number;
}
