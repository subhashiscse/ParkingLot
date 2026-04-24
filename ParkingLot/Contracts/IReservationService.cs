using System.Collections.Generic;

public interface IReservationService
{
    void Create(Reservation reservation);
    Reservation GetById(int id);
    List<Reservation> GetByCustomerId(int customerId);
    void Update(Reservation reservation);
    List<Reservation> GetAll();
    void DeleteAllByGarageID(int garageId);
}