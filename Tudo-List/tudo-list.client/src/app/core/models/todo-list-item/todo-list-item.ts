import { PriorityEnum } from "../../enums/priority-enum/priority-enum";
import { StatusEnum } from "../../enums/status-enum/status-enum";

export class TodoListItem {
    id!: string;
    itemId!: string;
    title!: string;
    description!: string;
    status!: StatusEnum;
    priority!: PriorityEnum;
    creationDate!: Date;
    userId!: number;
}
