﻿using GogoKit.Models.Request;
using GogoKit.Models.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GogoKit.Clients
{
    public interface IShipmentsClient
    {
        Task<Shipments> GetAsync(int saleId);

        Task<Shipments> GetAsync(int saleId, ShipmentRequest request);

        Task<IReadOnlyList<Shipment>> GetAllAsync(int saleId);

        Task<IReadOnlyList<Shipment>> GetAllAsync(int saleId, ShipmentRequest request);

        Task<IReadOnlyList<Shipment>> GetAllAsync(
            int saleId,
            ShipmentRequest request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Create a shipping label for the ticket(s) of a <see cref="Sale"/>.
        /// </summary>
        /// <param name="saleId">The identifier of the <see cref="Sale"/>.</param>
        /// <returns>The newly created <see cref="Shipment"/></returns>
        Task<Shipment> CreateAsync(int saleId);

        /// <summary>
        /// Create a shipping label for the ticket(s) of a <see cref="Sale"/>.
        /// </summary>
        /// <param name="saleId">The identifier of the <see cref="Sale"/>.</param>
        /// <param name="request">The parameters for this request</param>
        /// <returns>The newly created <see cref="Shipment"/></returns>
        Task<Shipment> CreateAsync(int saleId, ShipmentRequest request);

        /// <summary>
        /// Create a shipping label for the ticket(s) of a <see cref="Sale"/>.
        /// </summary>
        /// <param name="saleId">The identifier of the <see cref="Sale"/>.</param>
        /// <param name="request">The parameters for this request.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>The newly created <see cref="Shipment"/></returns>
        Task<Shipment> CreateAsync(int saleId, ShipmentRequest request, CancellationToken cancellationToken);
    }
}
