export interface IResponse<T> {
    data: T;
    message: string;
    success: boolean;
}

export interface IPaginationResponse<T> {
    currentPage: number;
    pageSize: number;
    totalPages: number;
    totalItems: number;
    result: T[];
}