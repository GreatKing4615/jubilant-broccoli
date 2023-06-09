﻿using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.BusinessLogic.Contracts;

public interface IRestaurantService
{
    public Task ProcessOrder(Order order);
}